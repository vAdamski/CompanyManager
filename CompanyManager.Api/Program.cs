using CompanyManager;
using CompanyManager.Application;
using CompanyManager.Common.Settings;
using CompanyManager.Configurations;
using CompanyManager.Infrastructure;
using CompanyManager.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAppSettings();
builder.ConfigureSerilog(builder.Configuration);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi();

// builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigurePolices();
builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.ConfigureCors("CORS");
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
	.AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();