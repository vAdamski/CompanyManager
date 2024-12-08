using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Domain.Events;
using CompanyManager.Shared.ServiceBusDtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;

public class EmployeeCreatedEventHandler(
	IAzureServiceBusSender azureServiceBusSender,
	ILogger<EmployeeCreatedEventHandler> logger)
	: INotificationHandler<CreateEmployeeInIdsEvent>
{
	public async Task Handle(CreateEmployeeInIdsEvent notification, CancellationToken cancellationToken)
	{
		// Send request to IDS to create user
		logger.LogDebug("EmployeeCreatedEventHandler: Handling EmployeeCreatedEvent");

		try
		{
			await azureServiceBusSender.SendAsync(new CreateUserInIdsRequest(notification));
		}
		catch (Exception e)
		{
			logger.LogError(e, "EmployeeCreatedEventHandler: Error while sending message to IDS queue");
			throw;
		}
	}
}