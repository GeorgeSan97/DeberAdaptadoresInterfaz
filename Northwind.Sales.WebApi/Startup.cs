using NorthWind.Sales.Backend.DataContexts.EFCore.Options;


namespace Northwind.Sales.WebApi
{
	internal static class Startup
	{
		public static WebApplication CreateWebaApplication(this WebApplicationBuilder builder)
		{
			
			//configurar el APIExplorer para descubrir y exponer 
			//los metadatos de los "endpints" de la aplicacion.
			builder.Services.AddEndpointsApiExplorer();

			// Agregar el generador que construye los objetos de
			// documentación de Swagger que tenga la funcionalidad de: ApiExplorer
			builder.Services.AddSwaggerGen();


			// Registrar los servicios de la aplicación que se va a exponer
			builder.Services.AddNorthWindSalesServices(
				dbOptions => builder.Configuration.GetSection(DBOptions.SectionKey).Bind(dbOptions));

			//Agregar el Servicio CORS para los clientes (Web, Windows, Móvil, etc. ) que se ejecuten
			//en el navegador Web (Blazor, React, Angular)
			builder.Services.AddCors(options => 
			{
				options.AddDefaultPolicy(config =>
				{
					config.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();

				});

			});

			return builder.Build();

		}

		public static WebApplication ConfigureWebApplication(this WebApplication app){
			app.UseExceptionHandler(builder => { });
			// Habilitar el middleware para que se muestre la información en fomrato JSON
			// y la interfaz UI de Swagger lo pueda viusalizar en el desarrollo
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();

				// Registrar los endpoints (urk) de la aplicación 
				app.MapNorthWindSalesEndpoints();

				// Agregar el Middleware CORS
				app.UseCors();
			}

			return app;
		}


	}
}
