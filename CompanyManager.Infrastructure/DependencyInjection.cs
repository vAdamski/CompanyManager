using CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;
using CompanyManager.Application.Common.Interfaces.Application.Helpers;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Infrastructure.AzureServiceBus;
using CompanyManager.Infrastructure.Helpers;
using CompanyManager.Infrastructure.Services;
using CompanyManager.Shared.ServiceBusDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddTransient<IVariablesGetter, VariablesGetter>();

		services.AddAzureServiceBusSender(cfg =>
		{
			var connectionString = configuration.GetConnectionString("AzureServiceBus");

			cfg.AddQueue(connectionString, typeof(CreateUserInIdsRequest), "CompanyManager.IdentityServer.CreateUser");
		});

		return services;
	}
}