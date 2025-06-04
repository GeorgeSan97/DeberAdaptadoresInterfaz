using Microsoft.Extensions.Options;
using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.DataContexts.EFCore.DataContexts;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using NorthWind.Sales.Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.Services
{
	internal class NorthWindSalesCommandsDataContext(
	IOptions<DBOptions> dbOptions) :
	NorthWindSalesContext(dbOptions),
	INorthWindSalesCommandsDataContext
	{
		public async Task AddOrderAsync(Order order) =>
	   await AddAsync(order);
		public async Task AddOrderDetailsAsync(
	   IEnumerable<Repositories.Entities.OrderDetail> orderDetails) =>
	   await AddRangeAsync(orderDetails);
		public async Task SaveChangesAsync() =>
	   await base.SaveChangesAsync();
	}
}
