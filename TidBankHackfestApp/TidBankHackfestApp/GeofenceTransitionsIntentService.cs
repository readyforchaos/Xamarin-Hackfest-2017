using Android.App;
using Android.Gms.Location;
using Android.Content;
using System.Collections.Generic;
using Android.Graphics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace TidBankHackfestApp
{
	[Service]
	public class GeofenceTransitionsIntentService : IntentService
	{
		protected const string TAG = "geofence-transitions-service";

		public GeofenceTransitionsIntentService() : base(TAG)
		{
		}

		protected override void OnHandleIntent(Intent intent)
		{
			var geofencingEvent = GeofencingEvent.FromIntent(intent);
			if (geofencingEvent.HasError)
				return;

			var geofenceTransition = geofencingEvent.GeofenceTransition;
			if (geofenceTransition == Geofence.GeofenceTransitionEnter ||
				geofenceTransition == Geofence.GeofenceTransitionExit)
			{
				var triggeringGeofences = geofencingEvent.TriggeringGeofences;
				var geofenceTransitionDetails = GetGeofenceTransitionDetails(this, geofenceTransition, triggeringGeofences);
				SendNotification(geofenceTransitionDetails);

				var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tidbankhackfest;AccountKey=zim7KI8GmnxY/ybkSQqzf6HKUw7coKNUlx7Wto7V6EsS/cLJpBY4mJLffrZd7NY6CP7DNTnV0f0SbO1G6g5+dA==;");
				var queueClient = storageAccount.CreateCloudQueueClient();
				var queue = queueClient.GetQueueReference("officequeue");
				if (geofenceTransition == Geofence.GeofenceTransitionEnter)
				{
					queue.AddMessageAsync(new CloudQueueMessage("AntonS checked in")).Wait();
				}
				else 
				{ 
					queue.ClearAsync().Wait();
				}
			}
		}

		string GetGeofenceTransitionDetails(Context context, int geofenceTransition, IList<IGeofence> triggeringGeofences)
		{
			var geofenceTransitionFormatString = GetTransitionString(geofenceTransition);

			var triggeringGeofencesIdsList = new List<string>();
			foreach (IGeofence geofence in triggeringGeofences)
			{
				triggeringGeofencesIdsList.Add(geofence.RequestId);
			}
			var triggeringGeofencesIdsString = string.Join(", ", triggeringGeofencesIdsList);

			return string.Format(geofenceTransitionFormatString, triggeringGeofencesIdsString);
		}

		void SendNotification(string notificationDetails)
		{

			var builder = new Notification.Builder(this);
			builder.SetSmallIcon(Resource.Drawable.Icon)
				   .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon))
				   .SetContentTitle("Automatic tracking")
				   .SetContentText(notificationDetails)
				   .SetAutoCancel(true);


			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.Notify(0, builder.Build());
		}

		string GetTransitionString(int transitionType)
		{
			switch (transitionType)
			{
				case Geofence.GeofenceTransitionEnter:
					return "You was automagically checked in due to entering {0} zone";
				case Geofence.GeofenceTransitionExit:
					return "You was automagically checked out due to leaving {0} zone";
				default:
					return "Unknown {0}";
			}
		}
	}
}

