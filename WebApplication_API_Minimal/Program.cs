using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;

using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NorthWind Sales API",
		Version = "v1"
	});
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});



builder.Services.AddNorthWindSalesServices(options => builder.Configuration.GetSection(DBOptions.SectionKey).Bind(options),
										SmtpOptions => builder.Configuration.GetSection(DBOptions.SectionKey).Bind(SmtpOptions));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

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
