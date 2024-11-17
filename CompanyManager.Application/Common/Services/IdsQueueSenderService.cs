using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;

namespace CompanyManager.Application.Common.Services;

public class IdsQueueSenderService(IQueueMessageSender queueMessageSender) : IIdsQueueSenderService
{
	public async Task SendMessageToIdsQueueAsync(string message)
	{
		await queueMessageSender.SendAsync(message, "idsQueue");
	}
}