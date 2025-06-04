using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.Configurations
{
	internal class OrderDetailConfiguration :
 IEntityTypeConfiguration<Repositories.Entities.OrderDetail>
	{
		public void Configure(
	   EntityTypeBuilder<Repositories.Entities.OrderDetail> builder)
		{
			builder.HasKey(d => new { d.OrderId, d.ProductId });
			builder.Property(d => d.UnitPrice)
			.HasPrecision(8, 2);
		}
	}
}
