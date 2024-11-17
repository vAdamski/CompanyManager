using CompanyManager.Infrastructure.RabbitMq.Abstractions;
using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq;

public class RabbitMqChannel(IRabbitMqConnection rabbitMqConnection) : IRabbitMqChannel
{
	public async Task<IChannel> CreateChannelAsync()
	{
		var connection = await rabbitMqConnection.GetConnectionAsync();
		var x = await connection.CreateChannelAsync();

		return x;
	}
}