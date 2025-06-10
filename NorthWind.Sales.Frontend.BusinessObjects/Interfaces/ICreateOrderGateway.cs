using NorthWind.Sales.Entities.Dtos.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Frontend.BusinessObjects.Interfaces
{
	public interface ICreateOrderGateway
	{
		Task<int> CreateOrderAsync(CreateOrderDto order);
	}
}
