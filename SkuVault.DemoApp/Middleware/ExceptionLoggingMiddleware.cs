using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace SkuVault.DemoApp.Middleware;

public class ExceptionLoggingMiddleware:IFunctionsWorkerMiddleware
{
	private readonly ILogger<ExceptionLoggingMiddleware> _logger;

	public ExceptionLoggingMiddleware(ILogger<ExceptionLoggingMiddleware> logger)
	{
		_logger = logger;
	}
	public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception e)
		{
			// ReSharper disable once TemplateIsNotCompileTimeConstantProblem
			_logger.LogWarning(e.Message);
		}
	}
}