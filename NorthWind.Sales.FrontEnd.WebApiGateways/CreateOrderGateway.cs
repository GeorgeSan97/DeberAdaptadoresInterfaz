using NorthWind. Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Entities.ValueObjects;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;

using System.Net.Http.Json;

namespace NorthWind.Sales.FrontEnd.WebApiGateways
{
	public class CreateOrderGateway(HttpClient client) : ICreateOrderGateway
	{
		int OrderId = 0;

		public async Task<int> CreateOrderAsync(CreateOrderDto order)
		{
			var Response = await client.PostAsJsonAsync(Endpoints.CreateOrder, order);

			if (Response.IsSuccessStatusCode)
			{
				OrderId = await Response.Content.ReadFromJsonAsync<int>();
			}
			else
			{

			}
			return OrderId;
		}

	

	}
}
