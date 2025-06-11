using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NorthWind.Sales.BlazorClient;
using NorthWind.Sales.Frontend.IoC;

public static class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		string? webApiAddress = builder.Configuration["WebApiAddress"];
		if (string.IsNullOrEmpty(webApiAddress))
		{
			throw new InvalidOperationException("La dirección de la API web no está configurada en 'appsettings.json'.");
		}

		builder.Services.AddNorthWindSalesServices(client =>
		{
			client.BaseAddress = new Uri(webApiAddress);
		});

		await builder.Build().RunAsync();
	}
}