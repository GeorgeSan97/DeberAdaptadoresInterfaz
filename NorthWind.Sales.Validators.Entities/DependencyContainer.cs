using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Validators.Entities.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyContainer
	{
		public static IServiceCollection AddValidators(
		this IServiceCollection services)
		{
			services.AddModelValidator<CreateOrderDto,
			CreateOrderDtoValidator>();
			services.AddModelValidator<CreateOrderDetailDto,
			CreateOrderDetailDtoValidator>();
			return services;
		}
	}
}
