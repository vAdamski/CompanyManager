using Duende.IdentityServer;
using CompanyManager.IdentityServer.Data;
using CompanyManager.IdentityServer.Models;
using CompanyManager.IdentityServer.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CompanyManager.IdentityServer;

internal static class HostingExtensions
{
	public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddTransient<IProfileService, ProfileService>();
		builder.Services.AddRazorPages();

		var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
		                       builder.Configuration.GetConnectionString("DefaultConnection");
		
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

		builder.Services
			.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
				options.EmitStaticAudienceClaim = true;
			})
			.AddInMemoryIdentityResources(Config.IdentityResources)
			.AddInMemoryApiScopes(Config.ApiScopes)
			.AddInMemoryClients(Config.Clients)
			.AddAspNetIdentity<ApplicationUser>()
			.AddProfileService<ProfileService>();

		builder.Services.AddAuthentication();
		
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("CORS", policy => policy
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod()
				.Build());
		});

		return builder.Build();
	}

	public static WebApplication ConfigurePipeline(this WebApplication app)
	{
		app.UseSerilogRequestLogging();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseStaticFiles();
		app.UseRouting();
		app.UseIdentityServer();

		app.UseCors("CORS");

		app.UseAuthorization();

		app.MapRazorPages()
			.AllowAnonymous();

		return app;
	}
}