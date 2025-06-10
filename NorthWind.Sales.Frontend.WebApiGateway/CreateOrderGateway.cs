using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Entities.ValueObjects;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Frontend.WebApiGateways
{
	internal class CreateOrderGateway(HttpClient client) : ICreateOrderGateway
	{
		public async Task<int> CreateOrderAsync(CreateOrderDto order)
		{
			int orderId = 0;
			var Response = await client.PostAsJsonAsync(
				Endpoints.CreateOrder, order);

			if (Response.IsSuccessStatusCode)
			{
				orderId = await Response.Content.ReadFromJsonAsync<int>();
			}
			return orderId;
		}
	}
}
