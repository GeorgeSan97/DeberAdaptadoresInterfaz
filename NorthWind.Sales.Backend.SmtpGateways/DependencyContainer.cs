using NorthWind.Entities.Interfaces;
using NorthWind.Sales.Backend.SmtpGateways;
using NorthWind.Sales.Backend.SmtpGateways.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyContainer
{
	public static IServiceCollection AddMailServices(
	this IServiceCollection services,
	Action<SmtpOptions> configureSmtpOptions)
	{
		services.AddSingleton<IMailService, MailService>();
		services.Configure(configureSmtpOptions);
		return services;
	}
}

