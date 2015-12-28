using Microsoft.Extensions.DependencyInjection;
using System;

namespace Compose.Tests.Emission
{
	public abstract class EmissionHelpers
	{
		protected static Service CreateProxy<Service, Implementation>()
			where Implementation : Service
		{
			var app = new Application();
			var serviceType = typeof(Service);
			app.UseServices(services => services.AddTransient(serviceType,typeof(Implementation)));
			AddProvider<Service, Implementation>(app);
			return (Service)app.ApplicationServices.GetRequiredService(serviceType);
		}

		private static void AddProvider<Service, Implementation>(Application app)
			where Implementation : Service
		{
			var providerInfo = typeof(TertiaryProviderExtensions).GetMethod("UseProvider", new[] { typeof(Application), typeof(Action<IServiceCollection>) }).MakeGenericMethod(typeof(Service));
			Action<IServiceCollection> serviceAction = services => services.AddTransient(typeof(Service), typeof(Implementation));
			providerInfo.Invoke(app, new object[] { app, serviceAction });
		}
	}
}
