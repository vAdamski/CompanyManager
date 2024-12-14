using CompanyManager.Services;
using Microsoft.OpenApi.Models;

namespace CompanyManager.Configurations;

public static class SwaggerConfiguration
{
	public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
	{
		var idsAuthority = Environment.GetEnvironmentVariable("IDS_AUTHORITY") ?? configuration["Ids:Authority"];

		services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows
				{
					AuthorizationCode = new OpenApiOAuthFlow
					{
						AuthorizationUrl = new Uri($"{idsAuthority}/connect/authorize"),
						TokenUrl = new Uri($"{idsAuthority}/connect/token"),
						Scopes = new Dictionary<string, string>
						{
							{ "api1", "Full access" },
							{ "openid", "openid" },
							{ "profile", "User info" },
						}
					}
				}
			});
			options.OperationFilter<AuthorizeCheckOperationFilter>();
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "CompanyManager API",
				Version = "v1",
				Description = "",
				TermsOfService = new Uri("https://example.com/terms"),
				Contact = new OpenApiContact
				{
					Name = "Adam Ludwiczak",
					Email = ""
				},
				License = new OpenApiLicense
				{
					Name = "Used License",
					Url = new Uri("https://example.com/license")
				}
			});
		});

		services.AddSwaggerGen();

		return services;
	}

	public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
			options.OAuthClientId("swagger");
			options.OAuthClientSecret("secret");
			options.OAuthUsePkce();
		});

		return app;
	}
}