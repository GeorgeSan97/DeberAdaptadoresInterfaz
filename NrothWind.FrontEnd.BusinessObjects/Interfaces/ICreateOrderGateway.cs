using NorthWind.Sales.Entities.Dtos.CreateOrder;


namespace NrothWind.FrontEnd.BusinessObjects.Interfaces
{
	public  interface ICreateOrderGateway
	{
		Task<int> CreateOrderGatewayAsync(CreateOrderDto order);
	}
}