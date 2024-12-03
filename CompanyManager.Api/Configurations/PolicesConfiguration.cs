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
		});

		return services;
	}
}