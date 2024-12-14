using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Domain.Events;
using CompanyManager.Shared.ServiceBusDtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeUpdated;

public class UpdateEmployeeInIdsEventHandler(
	IAzureServiceBusSender azureServiceBusSender,
	ILogger<UpdateEmployeeInIdsEventHandler> logger) : INotificationHandler<UpdateEmployeeInIdsEvent>
{
	public async Task Handle(UpdateEmployeeInIdsEvent notification, CancellationToken cancellationToken)
	{
		logger.LogDebug("EmployeeCreatedEventHandler: Handling EmployeeCreatedEvent");

		try
		{
			await azureServiceBusSender.SendAsync(new UpdateUserInIdsRequest(notification));
		}
		catch (Exception e)
		{
			logger.LogError(e, "EmployeeCreatedEventHandler: Error while sending message to IDS queue");
			throw;
		}
	}
}