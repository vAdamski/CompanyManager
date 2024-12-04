namespace CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;

public interface IAzureServiceBusSender
{
	Task SendAsync<T>(T message);
}