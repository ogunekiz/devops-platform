using DevOpsPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// 🔹 Swagger (NET 9 için şart)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Health Checks
builder.Services.AddHealthChecks()
		.AddNpgSql(
				builder.Configuration.GetConnectionString("Postgres"),
				name: "postgres");

var app = builder.Build();

// 🔹 Swagger UI
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "DevOpsPlatform API v1");
		options.RoutePrefix = "swagger"; // default
	});
}

app.UseHttpsRedirection();

// 🔹 Authorization
app.UseAuthorization();

// 🔹 Controller mapping (ŞART)
app.MapControllers();

// 🔹 Health endpoint
app.MapHealthChecks("/health");

app.Run();

