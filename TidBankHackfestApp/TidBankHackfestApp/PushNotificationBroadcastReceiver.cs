using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;


[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is needed only for Android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace TidBankHackfestApp
{

	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
		Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
		Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
		Categories = new string[] { "@PACKAGE_NAME@" })]

	public class PushNotificationBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
	{
		public static string[] SENDER_IDS = new string[] { Constants.SenderID };

		public const string TAG = "push-notifications-receiver";
	}


	[Service]
	public class PushHandlerService : GcmServiceBase
	{
		private NotificationHub _notificationHub { get; set; }

		public PushHandlerService() : base(Constants.SenderID)
		{
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			_notificationHub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
				context);
			try
			{
				_notificationHub.UnregisterAll(registrationId);
			}
			catch
			{
				//handle issues with Firebase registration 
			}

			//instead of tidbank create tag for every user
			var tags = new List<string> { "tidbank" };

			try
			{
				_notificationHub.Register(registrationId, tags.ToArray());
			}
			catch 
			{
			}
		}

		protected override void OnMessage(Context context, Intent intent)
		{
			var msg = new StringBuilder();

			if (intent != null && intent.Extras != null)
			{
				foreach (var key in intent.Extras.KeySet())
					msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
			}

			string messageText = intent.Extras.GetString("message");
			if (!string.IsNullOrEmpty(messageText))
			{
				CreateNotification("New hub message!", messageText);
			}
			else
			{
				CreateNotification("Unknown message details", msg.ToString());
			}
		}


		private void CreateNotification(string title, string desc)
		{
			var notificationManager = GetSystemService(NotificationService) as NotificationManager;

			var checkinIntent = new Intent(this, typeof(NotificationCheckinService));

			var notificationBuilder = new Notification.Builder(this)
				.SetSmallIcon(Android.Resource.Drawable.IcDialogAlert)
				.SetContentTitle(title)
				.AddAction(new Notification.Action(Android.Resource.Drawable.SymContactCard, "Check in", PendingIntent.GetService(this, 0, checkinIntent, PendingIntentFlags.UpdateCurrent)))
				.SetAutoCancel(true)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

			//Show the notification
			notificationManager.Notify(1, notificationBuilder.Build());
		}

		protected override void OnError(Context context, string errorId)
		{
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
		}
	}

}

