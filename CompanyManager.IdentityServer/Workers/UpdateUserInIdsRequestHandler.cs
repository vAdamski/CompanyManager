using CompanyManager.IdentityServer.Abstractions;
using CompanyManager.IdentityServer.Models;
using CompanyManager.Shared.ServiceBusDtos;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.IdentityServer.Workers;

public class UpdateUserInIdsRequestHandler(
	string connectionString,
	string queueName,
	IServiceProvider serviceProvider,
	ILogger<QueueProcessor<UpdateUserInIdsRequest>> logger)
	: QueueProcessor<UpdateUserInIdsRequest>(connectionString, queueName, logger)
{
	protected override async Task HandleMessageAsync(UpdateUserInIdsRequest message)
	{
		logger.LogDebug($"Received UpdateUserInIdsRequest. Message: {message}");
		
		using var scope = serviceProvider.CreateScope();
		var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		
		var user = await userMgr.FindByEmailAsync(message.Email);
		
		if (user == null)
		{
			logger.LogError($"User with id {message.Email} not found.");
			return;
		}
		
		user.FirstName = message.FirstName;
		user.LastName = message.LastName;
		user.UserName = message.UserName;
		
		await userMgr.UpdateAsync(user);
		
		logger.LogInformation($"User with id {message.Email} updated.");
	}
}