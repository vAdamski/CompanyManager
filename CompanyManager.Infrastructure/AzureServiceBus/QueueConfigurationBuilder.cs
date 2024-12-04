namespace CompanyManager.Infrastructure.AzureServiceBusSender;

public class QueueConfigurationBuilder
{
	internal Dictionary<Type, QueueConfiguration> QueueMappings { get; } = new();
	
	public void AddQueue(string connectionString, Type messageType, string queueName)
	{
		if (messageType == null)
			throw new ArgumentNullException(nameof(messageType));
		
		if (string.IsNullOrWhiteSpace(queueName))
			throw new ArgumentNullException(nameof(queueName));
		
		if (string.IsNullOrWhiteSpace(connectionString))
			throw new ArgumentNullException(nameof(connectionString));
		
		QueueMappings.Add(messageType, new QueueConfiguration(connectionString, queueName));
	}
}