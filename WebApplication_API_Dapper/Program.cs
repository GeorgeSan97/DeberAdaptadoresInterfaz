using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.Repositories.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NorthWind Sales API",
		Version = "v1"
	});
});

//CORS
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod());
});


 

builder.Services.Configure<DBOptions>(
	builder.Configuration.GetSection(DBOptions.SectionKey));


builder.Services.AddScoped<IDbConnection>(sp =>
{
	var configuration = sp.GetRequiredService<IConfiguration>();
	var connectionString = configuration
		.GetSection(DBOptions.SectionKey)
		.GetValue<string>(nameof(DBOptions.ConnectionString));

	if (string.IsNullOrWhiteSpace(connectionString))
		throw new InvalidOperationException(
			"Falta la cadena de conexión en DBOptions.ConnectionString.");

	return new SqlConnection(connectionString);
});


builder.Services.AddScoped<ICommandsRepository, CommandsRepository>();

 
builder.Services.AddNorthWindSalesServices(opts =>
	builder.Configuration.GetSection(DBOptions.SectionKey).Bind(opts));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/", () => "API funcionando. Usa /orders con POST para crear órdenes.");


app.MapPost("/orders", async ([FromServices] ICreateOrderInputPort inputPort,
							 [FromServices] ICreateOrderOutputPort outputPort,
							 [FromBody] CreateOrderDto orderDto) =>
{
	await inputPort.Handle(orderDto);
	return Results.Ok(new { OrderId = outputPort.OrderId });
})
.WithName("CreateOrder")
.WithOpenApi();


app.Run();
