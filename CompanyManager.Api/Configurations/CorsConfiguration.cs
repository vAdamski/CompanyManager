namespace CompanyManager.Configurations;

public static class CorsConfiguration
{
	public static IServiceCollection ConfigureCors(this IServiceCollection services, string corsPolicyName)
	{
		services.AddCors(options =>
		{
			options.AddPolicy(corsPolicyName, policy => policy
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod()
				.Build());
		});

		return services;
	}
}