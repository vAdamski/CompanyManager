using System.Text;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Infrastructure.RabbitMq.Abstractions;
using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq;

public class RabbitMqSender(IRabbitMqChannel rabbitMqChannel) : IQueueMessageSender
{
	public async Task SendAsync(string message, string queueName)
	{
		await using var channel = await rabbitMqChannel.CreateChannelAsync();
		await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
		
		var exchangeName = $"{queueName}-exchange";
		var routingKey = $"{queueName}-routing-key";

		await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
		await channel.QueueDeclareAsync(queueName, false, false, false, null);
		await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);

		var body = Encoding.UTF8.GetBytes(message);
		
		await channel.BasicPublishAsync(exchange: exchangeName, routingKey: routingKey, body: body);
	}
}