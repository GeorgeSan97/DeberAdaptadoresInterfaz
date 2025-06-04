using Microsoft.Extensions.DependencyInjection;
using NrothWind.FrontEnd.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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