namespace Northwind.Sales.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{

			WebApplication.CreateBuilder(args)
				.CreateWebaApplication()
				.ConfigureWebApplication()
				.Run();
		}
	}
}
