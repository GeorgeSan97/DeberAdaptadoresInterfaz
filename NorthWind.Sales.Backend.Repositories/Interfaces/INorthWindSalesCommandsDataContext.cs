using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.Repositories.Interfaces
{
	public interface INorthWindSalesCommandsDataContext
	{
		Task AddOrderAsync(Order order);
		Task AddOrderDetailsAsync(IEnumerable<Entities.OrderDetail> orderDetails);
		Task SaveChangesAsync();
	}
}
