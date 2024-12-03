using MassTransit;

namespace CompanyManager.Configurations;

public static class MassTransitConfiguration
{
	public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMassTransit(x =>
		{
			x.UsingAzureServiceBus((context, cfg) =>
			{
				cfg.Host(configuration.GetConnectionString("AzureServiceBus"));
			});
		});

		return services;
	}
}