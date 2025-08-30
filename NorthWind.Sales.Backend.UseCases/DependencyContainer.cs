using Microsoft.Extensions.DependencyInjection;
using NorthWind.Entities.Interfaces;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.UseCases.CreateOrder;
using NorthWind.Sales.Entities.Dtos.CreateOrder;

namespace NorthWind.Sales.Backend.UseCases;

public static class DependencyContainer
{
  public static IServiceCollection AddUserCasesServices(
	  this IServiceCollection services)
  {
    //  DI: Inyección de dependencias
    //  DIP: inyeccion y enversion 
    services.AddScoped<ICreateOrderInputPort, 
		CreateOrderInteractor>();
		
	services.AddModelValidator<CreateOrderDto,
	CreateOrderCustomerValidator>();
		
	services.AddModelValidator<CreateOrderDto,
	CreateOrderProductValidator>();

	services.AddScoped<IDomainEventHandler<SpecialOrderCreatedEvent>,
	SendEMailWhenSpecialOrderCreatedEventHandler>();


		// IoC Container: retorne el servicio con la inyeccion
		return services;
  }
}
