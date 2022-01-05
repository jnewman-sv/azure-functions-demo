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
	[Function("DemoHttpTrigger")]
	public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
		FunctionContext executionContext)
	{
		var logger = executionContext.GetLogger("DemoHttpTrigger");
		string responseString = "C# HTTP trigger function processed a request." + Environment.NewLine;

		var response = req.CreateResponse(HttpStatusCode.OK);
		response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
		responseString += "And is now responding....";
		response.WriteString(responseString);

		return response;
		
	}
}