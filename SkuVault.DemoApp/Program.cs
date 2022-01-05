using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using SkuVault.DemoApp.Middleware;

namespace SkuVault.DemoApp
{
	public static class Program
	{
		public static void Main()
		{
			var host = new HostBuilder()
				.ConfigureServices((context, collection) =>
				{
					// Registering Serilog provider
					Logger logger = new LoggerConfiguration()
						.WriteTo.Console(new JsonFormatter())
						.WriteTo.Debug(new JsonFormatter())
						.CreateLogger();
					collection.AddLogging(lb => lb.AddSerilog(logger));
				})
				.ConfigureFunctionsWorkerDefaults(builder =>
				{
					builder.UseMiddleware<LoggingMiddleware>();
					builder.UseMiddleware<ExceptionLoggingMiddleware>();
				})
				.Build();

			host.Run();
		}

	}
}