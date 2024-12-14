using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace CompanyManager.IdentityServer.Abstractions;

public abstract class QueueProcessor<TMessage>(
    string connectionString,
    string queueName,
    ILogger<QueueProcessor<TMessage>> logger)
    : BackgroundService
{
    private ServiceBusProcessor? _processor;

    protected abstract Task HandleMessageAsync(TMessage message);
    protected virtual Task HandleProcessingErrorAsync(ProcessErrorEventArgs args)
    {
        logger.LogError(args.Exception, "Error occurred in queue processor.");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = new ServiceBusClient(connectionString);
        _processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1
        });

        _processor.ProcessMessageAsync += ProcessMessageInternalAsync;
        _processor.ProcessErrorAsync += HandleProcessingErrorAsync;

        logger.LogInformation($"Starting queue processor for {queueName}...");
        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task ProcessMessageInternalAsync(ProcessMessageEventArgs args)
    {
        try
        {
            var messageBody = args.Message.Body.ToString();
            var message = JsonSerializer.Deserialize<TMessage>(messageBody);
            
            logger.LogDebug($"Received message: {messageBody}");            

            if (message == null)
            {
                logger.LogError("Failed to deserialize message.");
                await args.AbandonMessageAsync(args.Message);
                return;
            }

            await HandleMessageAsync(message);
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing message");
            await args.AbandonMessageAsync(args.Message);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"Stopping queue processor for {queueName}...");
        if (_processor != null)
        {
            await _processor.CloseAsync(stoppingToken);
        }
        await base.StopAsync(stoppingToken);
    }
}