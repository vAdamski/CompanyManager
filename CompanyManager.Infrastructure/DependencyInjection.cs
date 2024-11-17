using CompanyManager.Application.Common.Interfaces.Application.Helpers;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Abstractions;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Infrastructure.Helpers;
using CompanyManager.Infrastructure.RabbitMq;
using CompanyManager.Infrastructure.RabbitMq.Abstractions;
using CompanyManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddTransient<IRabbitMqConnection, RabbitMqConnection>();
		services.AddTransient<IRabbitMqChannel, RabbitMqChannel>();
		services.AddTransient<IQueueMessageSender, RabbitMqSender>();
		services.AddTransient<IVariablesGetter, VariablesGetter>();

		return services;
	}
}