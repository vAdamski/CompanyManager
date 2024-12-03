using CompanyManager.Application.Common.Interfaces.Api.Services;
using CompanyManager.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompanyManager;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
		
		return services;
	}
}