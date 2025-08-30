using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Backend.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static  class DependencyContainer
{
	public static IServiceCollection AddNorthWindSalesServices(
	this IServiceCollection services,
	Action<DBOptions> configureDBOptions)
	{
		services.AddUserCasesServices()
		.AddRepositories()
		.AddDataContexts(configureDBOptions)
		.AddPresenters()
		.AddValidationService()
		.AddValidators();
		


		return services;
	}
}


