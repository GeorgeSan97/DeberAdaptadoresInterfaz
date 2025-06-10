﻿using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;


namespace NorthWind.Sales.FrontEnd.WebApiGateways
{
	public static class DependencyContainer
	{
		public static IServiceCollection AddWebApiGateway(this IServiceCollection services, Action<HttpClient> configureHttpClient)
		{
			services.AddHttpClient<ICreateOrderGateway, CreateOrderGateway>(configureHttpClient);
			return services;
		}
	}
}