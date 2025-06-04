using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.DataContexts.EFCore.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.DataContexts
{
	internal class NorthWindSalesContext(IOptions<DBOptions> dbOptions) : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<Repositories.Entities.OrderDetail> OrderDetails { get; set; }
		protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(
			dbOptions.Value.ConnectionString);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly());
		}
	}
}