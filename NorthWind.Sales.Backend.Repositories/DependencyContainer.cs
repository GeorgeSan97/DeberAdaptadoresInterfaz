using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.Repositories.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
	public static IServiceCollection AddRepositories(
		this IServiceCollection services)
	{
		services.AddScoped<ICommandsRepository, CommandsRepository>();
		services.AddScoped<IQueriesRepository, QueriesRepository>();

		return services;
	}
}
