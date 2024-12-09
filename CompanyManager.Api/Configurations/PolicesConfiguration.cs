namespace CompanyManager.Configurations;

public static class PolicesConfiguration
{
	public static IServiceCollection ConfigurePolices(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("ApiScope", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
			});
			options.AddPolicy("User", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("User");
			});
			options.AddPolicy("CompanyOwner", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("User", "CompanyOwner");
			});
			options.AddPolicy("Admin", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("Admin");
			});
		});

		return services;
	}
}