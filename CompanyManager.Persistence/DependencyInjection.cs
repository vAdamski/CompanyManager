using CompanyManager.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
		                       configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString(connectionString)));

		services.AddScoped<IAppDbContext, AppDbContext>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}