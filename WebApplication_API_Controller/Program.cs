using NorthWind.Sales.Backend.DataContexts.EFCore.Options;


namespace Northwind.Sales.WebApiControllers
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddNorthWindSalesServices(options =>
			builder.Configuration.GetSection(DBOptions.SectionKey).Bind(options));

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.UseCreateOrderController();

			app.Run();
		}
	}
}
