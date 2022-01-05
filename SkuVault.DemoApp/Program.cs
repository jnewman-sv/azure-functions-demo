using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkuVault.DemoApp.Middleware;

namespace SkuVault.DemoApp
{
	public static class Program
	{
		public static void Main()
		{
			var host = new HostBuilder()
				/*.ConfigureServices((context, collection) =>
				{
					collection.AddLogging();
				})
				.ConfigureFunctionsWorkerDefaults(builder =>
				{
					builder.UseMiddleware<LoggingMiddleware>();
				})*/
				.Build();

			host.Run();
		}

	}
}