using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq.Abstractions;

public interface IRabbitMqConnection
{
	Task<IConnection> GetConnectionAsync();
}