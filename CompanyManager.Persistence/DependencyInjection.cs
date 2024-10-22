using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

		services.AddIdentityCore<ApplicationUser>()
			.AddEntityFrameworkStores<AppDbContext>();
		
		services.AddScoped<IAppDbContext, AppDbContext>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}