using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace CompanyManager.Configurations;

public static class AuthenticationConfiguration
{
	public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var idsAuthority = Environment.GetEnvironmentVariable("IDS_AUTHORITY") ?? configuration["Ids:Authority"];
		
		services.AddAuthentication("Bearer")
			.AddJwtBearer("Bearer", options =>
			{
				options.Authority = idsAuthority;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					NameClaimType = JwtClaimTypes.Name,
					RoleClaimType = JwtClaimTypes.Role
				};
			});
		
		return services;
	}
}