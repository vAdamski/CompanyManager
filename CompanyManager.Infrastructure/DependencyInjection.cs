using CompanyManager.Application.Common.Interfaces.Application.Helpers;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Infrastructure.Helpers;
using CompanyManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IDateTime, DateTimeService>();
		services.AddTransient<IVariablesGetter, VariablesGetter>();

		return services;
	}
}