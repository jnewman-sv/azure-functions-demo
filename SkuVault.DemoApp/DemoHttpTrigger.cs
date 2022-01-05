using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SkuVault.DemoApp;

public class DemoHttpTrigger
{
	private readonly ILogger<DemoHttpTrigger> _logger;

	public DemoHttpTrigger(ILogger<DemoHttpTrigger> logger)
	{
		_logger = logger;
	}

	[Function("DemoHttpTrigger")]
	[QueueOutput(ManyMessagesTrigger.ManyMessagesQueueName)]
	public IEnumerable<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
		FunctionContext executionContext)
	{
		string responseString = "C# HTTP trigger function processed a request." + Environment.NewLine;

		var response = req.CreateResponse(HttpStatusCode.OK);
		response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
		responseString += "And is now sending 10 messages into the queue....";
		
		response.WriteString(responseString);

		List<string> messages = new();
		messages.AddRange(Enumerable.Range(1,10).Select(i => i.ToString()));
		
		return messages;
		
	}
}