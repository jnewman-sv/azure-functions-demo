using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SkuVault.DemoApp;

public class ManyMessagesTrigger
{
	public const string ManyMessagesQueueName = "many-messages-queue";
	private readonly ILogger<ManyMessagesTrigger> _logger;

	public ManyMessagesTrigger(ILogger<ManyMessagesTrigger> logger)
	{
		_logger = logger;
	}

	[Function("ManyMessagesTrigger")]
	public void Run([QueueTrigger(ManyMessagesQueueName)] string myQueueItem,
		FunctionContext context)
	{
		_logger.LogInformation("Queue item received:{QueueItem}", myQueueItem);

		if (!int.TryParse(myQueueItem, out int value))
		{
			throw new ArgumentException("Not an integer");
		}

	}
}