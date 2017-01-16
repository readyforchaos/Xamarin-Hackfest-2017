using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace TidBankHackfestApp
{
	[Service]
	public class NotificationCheckinService : Service
	{
		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tidbankhackfest;AccountKey=zim7KI8GmnxY/ybkSQqzf6HKUw7coKNUlx7Wto7V6EsS/cLJpBY4mJLffrZd7NY6CP7DNTnV0f0SbO1G6g5+dA==;");
			var queueClient = storageAccount.CreateCloudQueueClient();
			var queue = queueClient.GetQueueReference("officequeue");
			queue.AddMessageAsync(new CloudQueueMessage("AntonS checked in")).Wait();
			return StartCommandResult.NotSticky;
		}
	}
}
