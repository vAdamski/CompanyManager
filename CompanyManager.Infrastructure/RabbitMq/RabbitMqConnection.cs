using CompanyManager.Infrastructure.RabbitMq.Abstractions;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq;

public class RabbitMqConnection(IConfiguration configuration) : IRabbitMqConnection
{
	private IConnection? _connection;

	public async Task<IConnection> GetConnectionAsync()
	{
		var uri = configuration["RabbitMq:Uri"] ?? throw new InvalidOperationException();
		var username = configuration["RabbitMq:Username"] ?? throw new InvalidOperationException();
		var password = configuration["RabbitMq:Password"] ?? throw new InvalidOperationException();
		var hostName = configuration["RabbitMq:Host"] ?? throw new InvalidOperationException();
		
		if (_connection == null || !_connection.IsOpen)
		{
			var factory = new ConnectionFactory()
			{
				UserName = username,
				Password = password,
				HostName = hostName
			};
			_connection = await factory.CreateConnectionAsync();
		}
		
		if (!_connection.IsOpen)
		{
			throw new InvalidOperationException("RabbitMQ connection has been not opened.");
		}

		return _connection;
	}
}