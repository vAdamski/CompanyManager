using CompanyManager.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger)
	: INotificationHandler<CreateEmployeeInIdsEvent>
{
	public async Task Handle(CreateEmployeeInIdsEvent notification, CancellationToken cancellationToken)
	{
		// Send request to IDS to create user
		logger.LogDebug("EmployeeCreatedEventHandler: Handling EmployeeCreatedEvent");
	}
}