using System.Text.Json;
using Azure.Messaging.ServiceBus;
using CompanyManager.IdentityServer.Common;
using CompanyManager.IdentityServer.Models;
using CompanyManager.Shared.ServiceBusDtos;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.IdentityServer.Workers;

public class CreateUserInIdsRequestHandler : BackgroundService
{
	private const string ConnectionString = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
	private const string QueueName = "CompanyManager.IdentityServer.CreateUser";
	
	private readonly ILogger<CreateUserInIdsRequestHandler> _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly ServiceBusProcessor _processor;

	public CreateUserInIdsRequestHandler(ILogger<CreateUserInIdsRequestHandler> logger, IServiceProvider serviceProvider)
	{
		_logger = logger;
		_serviceProvider = serviceProvider;

		var client = new ServiceBusClient(ConnectionString);
		_processor = client.CreateProcessor(QueueName, new ServiceBusProcessorOptions
		{
			AutoCompleteMessages = false,
			MaxConcurrentCalls = 1
		});
	}
	
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_processor.ProcessMessageAsync += OnMessageReceivedAsync;
		_processor.ProcessErrorAsync += OnErrorAsync;

		_logger.LogInformation("Starting Service Bus Listener...");
		await _processor.StartProcessingAsync(stoppingToken);
	}

	private async Task OnMessageReceivedAsync(ProcessMessageEventArgs args)
	{
		try
		{
			var messageBody = args.Message.Body.ToString();
			_logger.LogInformation($"Received message: {messageBody}");

			var myMessage = JsonSerializer.Deserialize<CreateUserInIdsRequest>(messageBody);
			
			if (myMessage == null)
			{
				_logger.LogError("Failed to deserialize message");
				await args.AbandonMessageAsync(args.Message);
				return;
			}

			await ProcessMessageAsync(myMessage);

			await args.CompleteMessageAsync(args.Message);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error processing message");
			await args.AbandonMessageAsync(args.Message);
		}
	}

	private Task OnErrorAsync(ProcessErrorEventArgs args)
	{
		_logger.LogError(args.Exception, "Error in Service Bus processing");
		return Task.CompletedTask;
	}

	private async Task ProcessMessageAsync(CreateUserInIdsRequest message)
	{
		_logger.LogInformation($"Processing message: {JsonSerializer.Serialize(message)}");

		using var scope = _serviceProvider.CreateScope();
		var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		var userBuilder = new UserBuilder(userMgr, roleMgr);
		
		await CreateUser(userBuilder, message);
	}

	private async Task CreateUser(UserBuilder userBuilder, CreateUserInIdsRequest message)
	{
		await userBuilder.CreateUserAsync(message.FirstName, message.LastName, message.Email, "Pass123$", message.Roles);
	}
	
	public override async Task StopAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Stopping Service Bus Listener...");
		await _processor.CloseAsync(stoppingToken);
		await base.StopAsync(stoppingToken);
	}
}