using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.UseCases.CreateOrder;

namespace NorthWind.Sales.Backend.UseCases;

public static class DependencyContainer
{
  public static IServiceCollection AddUserCasesServices(this IServiceCollection services)
  {
    //  DI: Inyección de dependencias
    //  DIP: inyeccion y enversion 
    services.AddScoped<ICreateOrderInputPort, CreateOrderInteractor>();

    // IoC Container: retorne el servicio con la inyeccion
    return services;
  }
}
