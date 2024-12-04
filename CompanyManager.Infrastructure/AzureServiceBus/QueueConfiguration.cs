namespace CompanyManager.Infrastructure.AzureServiceBusSender;

public class QueueConfiguration(string connectionString, string queueName)
{
	public string ConnectionString { get; } = connectionString;
	public string QueueName { get; } = queueName;
}