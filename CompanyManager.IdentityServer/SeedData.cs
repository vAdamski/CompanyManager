using System.Security.Claims;
using CompanyManager.IdentityServer.Common;
using IdentityModel;
using CompanyManager.IdentityServer.Data;
using CompanyManager.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CompanyManager.IdentityServer;

public class SeedData
{
	public static void EnsureSeedData(WebApplication app)
	{
		using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		context.Database.Migrate();

		var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		var userBuilder = new UserBuilder(userMgr, roleMgr);

		userBuilder.CreateUserAsync("Adam", "Ludwiczak", "adam.ludwiczak98@gmail.com", "Pass123$", ["User"]).Wait();
			
		Log.Information("Seeding complete");
	}
}