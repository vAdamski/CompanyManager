using CompanyManager.Domain.Events;
using MediatR;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
{
	public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
	{
		// Send request to IDS to create user

		return;
	}
}