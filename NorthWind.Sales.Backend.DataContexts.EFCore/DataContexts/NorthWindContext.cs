using Microsoft.EntityFrameworkCore;
using NorthWind.Sales.Backend.BusinessObjects.POCOEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.DataContexts.EFCore.DataContexts
{
	internal class NorthWindContext : DbContext
	{
		protected override void OnConfiguring(
	   DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(
			"Data Source=GEORGE-ASUS;Initial Catalog=NorthWindDB;Persist Security Info=True;User ID=sa;Password=jorgesa;TrustServerCertificate=True");
		}
		public DbSet<Order> Orders { get; set; }
		public DbSet<Repositories.Entities.OrderDetail> OrderDetails { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly());
		}

	}
}
