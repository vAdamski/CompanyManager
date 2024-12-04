using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Infrastructure.AzureServiceBusSender;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure.AzureServiceBus;

public static class AzureServiceBusExtension
{
	public static void AddAzureServiceBusSender(this IServiceCollection services,
		Action<QueueConfigurationBuilder> configure)
	{
		var builder = new QueueConfigurationBuilder();
		configure(builder);

		services.AddSingleton(builder);
		services.AddSingleton<IAzureServiceBusSender>(sp => new AzureServiceBusSender(builder.QueueMappings));
	}
}