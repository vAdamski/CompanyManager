using CompanyManager.Application.Common.Interfaces.Application.Helpers;
using CompanyManager.Infrastructure.RabbitMq.Abstractions;
using RabbitMQ.Client;

namespace CompanyManager.Infrastructure.RabbitMq;

public class RabbitMqConnection(IVariablesGetter variablesGetter) : IRabbitMqConnection
{
	private IConnection? _connection;

	public async Task<IConnection> GetConnectionAsync()
	{
		var username = variablesGetter.GetVariable("RabbitMq:Username", "RABBITMQ_USERNAME");
		var password = variablesGetter.GetVariable("RabbitMq:Password", "RABBITMQ_PASSWORD");
		var hostName = variablesGetter.GetVariable("RabbitMq:HostName", "RABBITMQ_HOSTNAME");
		
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