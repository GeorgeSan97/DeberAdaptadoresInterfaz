using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Frontend.WebApiGateways
{
	public static class DependencyContainer
	{
		public static IServiceCollection AddWebApiGateways(
			this IServiceCollection services, 
			Action<HttpClient> configureHttpClient)
		{
			services.AddHttpClient<ICreateOrderGateway,
				CreateOrderGateway>(configureHttpClient);
			return services;
		}
	}
}
