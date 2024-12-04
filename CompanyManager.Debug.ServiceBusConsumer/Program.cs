using System.Text;
using Azure.Messaging.ServiceBus;

var connectionString =
	"Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";

// Receive message from the queue CompanyManager.IdentityServer.CreateUser
await using (var client = new ServiceBusClient(connectionString))
{
	ServiceBusReceiver receiver = client.CreateReceiver("CompanyManager.IdentityServer.CreateUser");

	var messages = await receiver.ReceiveMessagesAsync(maxMessages: 10, maxWaitTime: TimeSpan.FromSeconds(5));
	
	foreach (ServiceBusReceivedMessage message in messages)
	{
		Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");
		// await receiver.CompleteMessageAsync(message);
	}
}