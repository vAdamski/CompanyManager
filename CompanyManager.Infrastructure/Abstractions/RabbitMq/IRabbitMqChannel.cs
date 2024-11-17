using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq.Abstractions;

public interface IRabbitMqChannel
{
	Task<IChannel> CreateChannelAsync();
}