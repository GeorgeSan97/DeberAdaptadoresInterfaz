﻿using NorthWind.Sales.Backend.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Entities.Dtos.CreateOrder;
using NorthWind.Sales.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder;

public static class CreateOrderController
{
	public static WebApplication UseCreateOrderController(
this WebApplication app)
	{
		app.MapPost(Endpoints.CreateOrder, CreateOrder);
		return app;
	}
	public static async Task<int> CreateOrder(
   CreateOrderDto orderDto,
   ICreateOrderInputPort inputPort,
   ICreateOrderOutputPort presenter)
	{
		await inputPort.Handle(orderDto);
		return presenter.OrderId;
	}
}
