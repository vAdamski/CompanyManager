using System.Reflection;
using CompanyManager.Application.Common.Behaviours;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
		services.AddTransient<IIdsQueueSenderService, IdsQueueSenderService>();
        
		return services;
	}
}