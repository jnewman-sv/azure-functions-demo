using System;
using System.Collections.Generic;
using System.Linq;
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
	[QueueOutput(ManyMessagesTrigger.ManyMessagesQueueName)]
	public IEnumerable<string> Run([QueueTrigger(ManyMessagesQueueName)] string myQueueItem,
		FunctionContext context)
	{
		_logger.LogInformation("Queue item received:{QueueItem}", myQueueItem);

		if (!int.TryParse(myQueueItem, out int value))
		{
			throw new ArgumentException("Not an integer");
		}

		switch (value)
		{
			case > 21:  // throw an exception if the value is greater than 21
				throw new IndexOutOfRangeException("Number is too high!");
			case 10: // if the value is exactly 10, send two more messages.
			{
				List<string> messages = new();
				messages.AddRange(Enumerable.Range(11, 2).Select(i => (value + i).ToString()));
				return messages;
			}
			default:
				// This is the end.  There is no message here, and nothing will be enqueued.
				return null;
		}
	}
}