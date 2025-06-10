using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Frontend.Views;
using NorthWind.Sales.Frontend.WebApiGateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Frontend.IoC
{
	public static class DependencyContainer
	{
		public static IServiceCollection AddNorthWindSalesServices(
		this IServiceCollection services,
		Action<HttpClient> configureHttpClient)
		{
			services.AddWebApiGateways(configureHttpClient)
			.AddViewsServices();
			return services;
		}
	}
}
