using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Infrastructure.AzureServiceBusSender;

namespace CompanyManager.Infrastructure.AzureServiceBus;

public class AzureServiceBusSender(Dictionary<Type, QueueConfiguration> queueMappings) : IAzureServiceBusSender
{
	public async Task SendAsync<T>(T message)
	{
		if (!queueMappings.TryGetValue(typeof(T), out var queueConfiguration))
			throw new InvalidOperationException($"Queue not mapped for message type {typeof(T).Name}");

		await using var client = new ServiceBusClient(queueConfiguration.ConnectionString);
		
		ServiceBusSender sender = client.CreateSender(queueConfiguration.QueueName);
			
		var messageToSend = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
		{
			ContentType = "application/json",
		};
			
		await sender.SendMessageAsync(messageToSend);
	}
}