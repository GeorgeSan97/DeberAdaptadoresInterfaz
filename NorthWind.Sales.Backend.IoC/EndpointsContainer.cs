using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
	public static class EndpointsContainer
	{
		public static WebApplication MapNorthWindSalesEndpoints(
		this WebApplication app)
		{
			app.UseCreateOrderController();
			return app;
		}
	}
}
