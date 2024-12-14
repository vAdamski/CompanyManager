using System.Text.Json;
using Azure.Messaging.ServiceBus;
using CompanyManager.IdentityServer.Abstractions;
using CompanyManager.IdentityServer.Common;
using CompanyManager.IdentityServer.Models;
using CompanyManager.Shared.ServiceBusDtos;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.IdentityServer.Workers;

public class CreateUserInIdsRequestHandler(
	string connectionString,
	string queueName,
	IServiceProvider serviceProvider,
	ILogger<CreateUserInIdsRequestHandler> logger)
	: QueueProcessor<CreateUserInIdsRequest>(connectionString, queueName, logger)
{
	protected override async Task HandleMessageAsync(CreateUserInIdsRequest message)
	{
		using var scope = serviceProvider.CreateScope();
		var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		var userBuilder = new UserBuilder(userMgr, roleMgr);
		await userBuilder.CreateUserAsync(
			message.FirstName, 
			message.LastName, 
			message.Email, 
			"Pass123$", 
			roles: message.Roles);
	}
}