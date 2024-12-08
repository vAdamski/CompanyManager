using CompanyManager.Application.Actions.EmployeeActions.Events.EmployeeCreated;
using CompanyManager.Shared.ServiceBusDtos;
using MassTransit;

namespace CompanyManager.Configurations;

public static class MassTransitConfiguration
{
	public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMassTransit(x =>
		{
			x.SetKebabCaseEndpointNameFormatter();
			
			x.UsingAzureServiceBus((context, cfg) =>
			{
				cfg.Host(configuration.GetConnectionString("AzureServiceBus"));

				cfg.Send<CreateUserInIdsRequest>(s => s.UseSessionIdFormatter(c => c.RequestId.ToString()));
			});
		});

		return services;
	}
}