using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Util;
using Android.Widget;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace TidBankHackfestApp
{
	[BroadcastReceiver(Label = "@string/widget_name")]
	[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
	[MetaData("android.appwidget.provider", Resource = "@xml/widget_info")]
	public class TidBankWidget : AppWidgetProvider
	{
		private const string ChangeCheckinType = "changeCheckinTypeClick";
		private const string CheckIn = "checkinTypeClick";

		public override void OnEnabled(Context context)
		{
			var remoteViews = new RemoteViews(context.PackageName, Resource.Layout.WidgetLayout);
			var widget = new ComponentName(context, Java.Lang.Class.FromType(typeof(TidBankWidget)).Name);
			remoteViews.SetOnClickPendingIntent(Resource.Id.selectCheckin, GetPendingSelfIntent(context, ChangeCheckinType));
			remoteViews.SetOnClickPendingIntent(Resource.Id.postCheckin, GetPendingSelfIntent(context, CheckIn));
			AppWidgetManager.GetInstance(context).UpdateAppWidget(widget, remoteViews);
		}

	public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
		{
			
    }

	
	public override void OnReceive(Context context, Intent intent)
	{
			base.OnReceive(context, intent);
			var appWidgetManager = AppWidgetManager.GetInstance(context);

			var remoteViews = new RemoteViews(context.PackageName, Resource.Layout.WidgetLayout);
			var watchWidget = new ComponentName(context, Java.Lang.Class.FromType(typeof(TidBankWidget)).Name);

			var prefs = Application.Context.GetSharedPreferences("tidbank", FileCreationMode.Private);

		if (ChangeCheckinType==intent.Action)
		{
				
				string newLabel;
				switch (prefs.GetString("CheckinMode", "Normal"))
				{
					case "Normal":
						newLabel = "Overtime";
						break;
					case "Overtime":
						newLabel = "Late";
						break;
						case "Late":
						newLabel = "Sick";
						break;
						case "Sick":
						newLabel = "Appointment";
							break;
						default:
						newLabel = "Normal";
							break;
				}
				var prefEditor = prefs.Edit();
				prefEditor.PutString("CheckinMode", newLabel);
				prefEditor.Commit();
				remoteViews.SetTextViewText(Resource.Id.selectCheckin, newLabel);

				appWidgetManager.UpdateAppWidget(watchWidget, remoteViews);
				return;
        }
			if (CheckIn == intent.Action)
			{
				var isCheckedIn = prefs.GetBoolean("IsCheckedIn", false);
				var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tidbankhackfest;AccountKey=zim7KI8GmnxY/ybkSQqzf6HKUw7coKNUlx7Wto7V6EsS/cLJpBY4mJLffrZd7NY6CP7DNTnV0f0SbO1G6g5+dA==;");
				var queueClient = storageAccount.CreateCloudQueueClient();
				var queue = queueClient.GetQueueReference("officequeue");
				var prefEditor = prefs.Edit();
				var newValue = !isCheckedIn;
				prefEditor.PutBoolean("IsCheckedIn", newValue);
				prefEditor.Commit();
				remoteViews.SetTextViewText(Resource.Id.postCheckin, isCheckedIn?"Check out":"Check in");
				appWidgetManager.UpdateAppWidget(watchWidget, remoteViews);
				if (isCheckedIn) 
				{
					queue.ClearAsync().Wait();
				}
				else
				{
					queue.AddMessageAsync(new CloudQueueMessage("AntonS checked in")).Wait();
				}
			}
    }

    protected PendingIntent GetPendingSelfIntent(Context context, string action)
{
			var intent = new Intent(context, typeof(TidBankWidget));
			intent.SetAction(action);
			return PendingIntent.GetBroadcast(context, 0, intent, 0);
}
	}
}
