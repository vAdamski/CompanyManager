using CompanyManager.Application;
using CompanyManager.Application.Common.Interfaces.Api.Services;
using CompanyManager.Common.Settings;
using CompanyManager.Infrastructure;
using CompanyManager.Persistence;
using CompanyManager.Services;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

var logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
        
		options.Authority = Environment.GetEnvironmentVariable("Ids__Authority") ?? builder.Configuration["Ids:Authority"];
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
			NameClaimType = JwtClaimTypes.Name,
			RoleClaimType = JwtClaimTypes.Role
		};
	});

builder.Services.AddAuthorization(options =>
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

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.OAuth2,
		Flows = new OpenApiOAuthFlows
		{
			AuthorizationCode = new OpenApiOAuthFlow
			{
				AuthorizationUrl = new Uri("https://localhost:6001/connect/authorize"),
				TokenUrl = new Uri("https://localhost:6001/connect/token"),
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

builder.Services.AddCors(options =>
{
	options.AddPolicy("CORS", policy => policy
		.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
	.AddJsonOptions(opt =>
	{
		opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
	options.OAuthClientId("swagger");
	options.OAuthClientSecret("secret");
	options.OAuthUsePkce();
});

app.UseHttpsRedirection();

app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();