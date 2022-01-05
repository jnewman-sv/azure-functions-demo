using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace SkuVault.DemoApp.Middleware;

public class LoggingMiddleware:IFunctionsWorkerMiddleware
{
	private readonly ILogger<LoggingMiddleware> _logger;

	public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
	{
		_logger = logger;
	}

	public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
	{
		_logger.BeginScope(new { timeStamp = DateTime.UtcNow.ToTimestamp() });
		// ReSharper disable once TemplateIsNotCompileTimeConstantProblem
		_logger.LogInformation($"Entering function: {context.FunctionDefinition.Name}-{context.FunctionDefinition.Id}");
		await next(context);
		_logger.LogInformation($"Function: {context.FunctionDefinition.Name}-{context.FunctionDefinition.Id} ... completed");
	}
}