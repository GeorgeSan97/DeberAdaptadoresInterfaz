using NorthWind.Sales.Entities.Dtos.CreateOrder;
using static NorthWind.Sales.Entities.Dtos.CreateOrder.CreateOrderDto;


namespace NrothWind.FrontEnd.BusinessObjects.Interfaces
{
	public  interface ICreateOrderGateway
	{
		//Task<int> CreateOrderGatewayAsync(CreateOrderDto order);
		Task<CreateOrderResponseDto> CreateOrderGatewayAsync(CreateOrderDto order);
	}
}