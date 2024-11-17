using CompanyManager.Domain.Events;
using MediatR;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class EmployeeCreatedEventHandler : INotificationHandler<CreateEmployeeInIdsEvent>
{
	public async Task Handle(CreateEmployeeInIdsEvent notification, CancellationToken cancellationToken)
	{
		// Send request to IDS to create user

		return;
	}
}