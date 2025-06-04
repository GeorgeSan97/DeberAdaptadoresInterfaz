using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.Repositories.Repositories;   // <-- CommandsRepository (ADO.NET)

var builder = WebApplication.CreateBuilder(args);

// ──────────────────────────────────────────────
//  Swagger / OpenAPI
// ──────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NorthWind Sales API",
		Version = "v1"
	});
});

// ──────────────────────────────────────────────
//  CORS
// ──────────────────────────────────────────────
builder.Services.AddCors(opt =>
{
	opt.AddDefaultPolicy(p =>
		p.AllowAnyOrigin()
		 .AllowAnyHeader()
		 .AllowAnyMethod());
});

// ──────────────────────────────────────────────
//  Opciones (DBOptions)  – sección “DBOptions” en appsettings.json
// ──────────────────────────────────────────────
builder.Services.Configure<DBOptions>(
	builder.Configuration.GetSection(DBOptions.SectionKey));

// ──────────────────────────────────────────────
//  SqlConnection (ADO.NET)  –  ciclo de vida Scoped
// ──────────────────────────────────────────────
builder.Services.AddScoped<IDbConnection>(sp =>
{
	var cfg = sp.GetRequiredService<IConfiguration>();
	var connStr = cfg.GetSection(DBOptions.SectionKey)
					   .GetValue<string>(nameof(DBOptions.ConnectionString));

	if (string.IsNullOrWhiteSpace(connStr))
		throw new InvalidOperationException("Falta DBOptions:ConnectionString en appsettings.json.");

	// Microsoft.Data.SqlClient es el proveedor recomendado para .NET 6+ / .NET 8
	return new SqlConnection(connStr);
});

// ──────────────────────────────────────────────
//  Repositorios y casos de uso
// ──────────────────────────────────────────────
builder.Services.AddScoped<ICommandsRepository, CommandsRepository>();

// Registra el resto de servicios de NorthWind (ya existentes)
builder.Services.AddNorthWindSalesServices(opts =>
	builder.Configuration.GetSection(DBOptions.SectionKey).Bind(opts));

// ──────────────────────────────────────────────
//  Build & middleware
// ──────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();   // launchUrl = "swagger" en launchSettings.json
}

app.UseCors();

// ──────────────────────────────────────────────
//  Endpoints
// ──────────────────────────────────────────────
app.MapPost("/orders", async (
	[FromBody] CreateOrderDto orderDto,
	ICreateOrderInputPort inputPort,
	ICreateOrderOutputPort outputPort) =>
{
	await inputPort.Handle(orderDto);
	return Results.Ok(new { OrderId = outputPort.OrderId });
})
.WithName("CreateOrder")
.WithOpenApi();

app.Run();
