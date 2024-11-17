using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class EmployeeCreatedEventHandler(
	IIdsQueueSenderService idsQueueSenderService,
	ILogger<EmployeeCreatedEventHandler> logger)
	: INotificationHandler<CreateEmployeeInIdsEvent>
{
	public async Task Handle(CreateEmployeeInIdsEvent notification, CancellationToken cancellationToken)
	{
		// Send request to IDS to create user
		logger.LogDebug("EmployeeCreatedEventHandler: Handling EmployeeCreatedEvent");

		await idsQueueSenderService.SendMessageToIdsQueueAsync(new CreateUserInIdsRequest(notification).ToJson());
	}
}