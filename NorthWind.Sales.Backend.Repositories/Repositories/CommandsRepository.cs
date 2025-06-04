// Adonet
using Microsoft.Data.SqlClient;
using System.Data;
using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;

namespace NorthWind.Sales.Backend.Repositories.Repositories
{
	/// <summary>
	/// Versión ADO.NET pura del repositorio de comandos.
	/// Mantiene la misma lógica (transacción única, métodos asíncronos) pero sin Dapper.
	/// </summary>
	public sealed class CommandsRepository : ICommandsRepository
	{
		private readonly SqlConnection _connection;
		private readonly SqlTransaction _transaction;

		public CommandsRepository(IDbConnection connection)
		{
			// ‼️ Requerimos SqlConnection para poder usar métodos async de ADO.NET
			if (connection is not SqlConnection sqlConn)
				throw new ArgumentException("CommandsRepository requiere una SqlConnection", nameof(connection));

			_connection = sqlConn;

			if (_connection.State != ConnectionState.Open)
				_connection.Open();

			_transaction = _connection.BeginTransaction();
		}

		public async Task CreateOrder(OrderAggregate order)
		{
			if (order is null) throw new ArgumentNullException(nameof(order));
			if (order.OrderDate == default) order.OrderDate = DateTime.UtcNow;

			// ---------- INSERT ÓRDEN ----------
			const string insertOrderSql = @"
INSERT INTO Orders(
    CustomerId,
    ShipAddress,
    ShipCity,
    ShipCountry,
    ShipPostalCode,
    ShippingType,
    DiscountType,
    Discount,
    OrderDate)
VALUES (
    @CustomerId,
    @ShipAddress,
    @ShipCity,
    @ShipCountry,
    @ShipPostalCode,
    @ShippingType,
    @DiscountType,
    @Discount,
    @OrderDate);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

			int orderId;
			using (var cmd = new SqlCommand(insertOrderSql, _connection, _transaction))
			{
				cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
				cmd.Parameters.AddWithValue("@ShipAddress", order.ShipAddress ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ShipCity", order.ShipCity ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ShipCountry", order.ShipCountry ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ShipPostalCode", order.ShipPostalCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ShippingType", order.ShippingType);
				cmd.Parameters.AddWithValue("@DiscountType", order.DiscountType);
				cmd.Parameters.AddWithValue("@Discount", order.Discount);
				cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);

				orderId = (int)await cmd.ExecuteScalarAsync();
			}

			order.Id = orderId;

			// ---------- INSERT DETALLES ----------
			const string insertDetailSql = @"
INSERT INTO OrderDetails(
    OrderId,
    ProductId,
    UnitPrice,
    Quantity)
VALUES (
    @OrderId,
    @ProductId,
    @UnitPrice,
    @Quantity);";

			foreach (var d in order.OrderDetails)
			{
				using var detailCmd = new SqlCommand(insertDetailSql, _connection, _transaction);
				detailCmd.Parameters.AddWithValue("@OrderId", orderId);
				detailCmd.Parameters.AddWithValue("@ProductId", d.ProductId);
				detailCmd.Parameters.AddWithValue("@UnitPrice", d.UnitPrice);
				detailCmd.Parameters.AddWithValue("@Quantity", d.Quantity);

				await detailCmd.ExecuteNonQueryAsync();
			}
		}

		public Task SaveChanges()
		{
			try
			{
				_transaction.Commit();
			}
			catch
			{
				_transaction.Rollback();
				throw;
			}
			finally
			{
				_transaction.Dispose();
				_connection.Close();
			}

			return Task.CompletedTask;
		}
	}
}




/*
//Dapper
using Dapper;
using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using System.Data;

namespace NorthWind.Sales.Backend.Repositories.Repositories
{
	public sealed class CommandsRepository : ICommandsRepository
	{
		private readonly IDbConnection _connection;
		private readonly IDbTransaction _transaction;

		public CommandsRepository(IDbConnection connection)
		{
			_connection = connection ?? throw new ArgumentNullException(nameof(connection));

			if (_connection.State != ConnectionState.Open)
				_connection.Open();

			_transaction = _connection.BeginTransaction();
		}

		public async Task CreateOrder(OrderAggregate order)
		{
			ArgumentNullException.ThrowIfNull(order);

			// Asegurar fecha de orden
			// The error CS0019 occurs because the null-coalescing assignment operator (??=) cannot be used with non-nullable value types like DateTime.
			// To fix this, we need to use a conditional check instead.

			if (order.OrderDate == default)
			{
				order.OrderDate = DateTime.UtcNow;
			}


			const string insertOrderSql = @"
INSERT INTO Orders(
    CustomerId,
    ShipAddress,
    ShipCity,
    ShipCountry,
    ShipPostalCode,
    ShippingType,
    DiscountType,
    Discount,
    OrderDate)
VALUES (
    @CustomerId,
    @ShipAddress,
    @ShipCity,
    @ShipCountry,
    @ShipPostalCode,
    @ShippingType,
    @DiscountType,
    @Discount,
    @OrderDate);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

			var orderId = await _connection.ExecuteScalarAsync<int>(
				insertOrderSql,
				new
				{
					order.CustomerId,
					order.ShipAddress,
					order.ShipCity,
					order.ShipCountry,
					order.ShipPostalCode,
					order.ShippingType,
					order.DiscountType,
					order.Discount,
					order.OrderDate
				},
				_transaction);

			order.Id = orderId;

			const string insertDetailSql = @"
INSERT INTO OrderDetails(
    OrderId,
    ProductId,
    UnitPrice,
    Quantity)
VALUES (
    @OrderId,
    @ProductId,
    @UnitPrice,
    @Quantity);";

			var detailParameters = order.OrderDetails.Select(d => new
			{
				OrderId = orderId,
				d.ProductId,
				d.UnitPrice,
				d.Quantity
			});

			await _connection.ExecuteAsync(insertDetailSql, detailParameters, _transaction);
		}

		public Task SaveChanges()
		{
			try
			{
				_transaction.Commit();
			}
			catch
			{
				_transaction.Rollback();
				throw;
			}
			finally
			{
				_transaction.Dispose();
				_connection.Close();
			}

			return Task.CompletedTask;
		}
	}
}
*/



/*
// Hecho en clases 
using NorthWind.Sales.Backend.BusinessObjects.Aggregates;
using NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Sales.Backend.Repositories.Repositories
{
	internal class CommandsRepository(INorthWindSalesCommandsDataContext context) : ICommandsRepository
	{
		public async Task CreateOrder(OrderAggregate order)
		{
			await context.AddOrderAsync(order);
			await context.AddOrderDetailsAsync(
				order.OrderDetails
				.Select (d => new Entities.OrderDetail
				{
					Order = order,
					ProductId = d.ProductId,
					Quantity = d.Quantity,
					UnitPrice = d.UnitPrice
				}).ToArray());
		}

	

		public async Task SaveChanges() =>
			await context.SaveChangesAsync();
	}
}
*/