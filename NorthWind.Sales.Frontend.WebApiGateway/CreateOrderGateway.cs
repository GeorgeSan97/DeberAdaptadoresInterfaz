using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Entities.ValueObjects;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static NorthWind.Sales.Entities.Dtos.CreateOrder.CreateOrderDto;


namespace NorthWind.Sales.Frontend.WebApiGateways
{
	internal class CreateOrderGateway(HttpClient client) : ICreateOrderGateway
	{
		public async Task<int> CreateOrderAsync(CreateOrderDto order)
		{
			CreateOrderResponseDto result = null;

			var response = await client.PostAsJsonAsync(Endpoints.CreateOrder, order);

			if (response.IsSuccessStatusCode)
			{
				result = await response.Content.ReadFromJsonAsync<CreateOrderResponseDto>();
			}
			else
			{
				throw new HttpRequestException(
				await response.Content.ReadAsStringAsync());
			}

			// Fix: Return the OrderId property from the CreateOrderResponseDto object
			return result?.OrderId ?? 0;
		}
	}
}
