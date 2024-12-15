using CompanyManager.Common.Helpers;

namespace CompanyManager.Configurations;

public static class PolicyConfiguration
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
			options.AddPolicy(PolicyValues.Employee, policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("User");
			});
			options.AddPolicy(PolicyValues.CompanyOwner, policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("User", "CompanyOwner");
			});
			options.AddPolicy(PolicyValues.Admin, policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("Admin");
			});
			options.AddPolicy(PolicyValues.AdminOrCompanyOwner, policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "api1");
				policy.RequireRole("Admin", "CompanyOwner");
			});
		});

		return services;
	}
}