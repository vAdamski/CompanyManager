namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface IIdsQueueSenderService
{
	Task SendMessageToIdsQueueAsync(string message);
}