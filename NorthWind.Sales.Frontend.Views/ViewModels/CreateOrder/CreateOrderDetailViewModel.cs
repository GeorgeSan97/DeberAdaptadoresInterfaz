﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Frontend.Views.ViewModels.CreateOrder
{
	public class CreateOrderDetailViewModel
	{
		public int ProductId { get; set; }

		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
	
	}
}
