namespace CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;

public interface IQueueMessageSender
{
	Task SendAsync(string message, string queueName);
}