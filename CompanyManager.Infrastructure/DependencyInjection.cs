using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IDateTime, DateTimeService>();

		return services;
	}
}