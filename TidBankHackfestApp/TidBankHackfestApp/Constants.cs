using System.Collections.Generic;
using Android.Gms.Maps.Model;

namespace TidBankHackfestApp
{
	public static class Constants
	{

		public const long GeofenceExpirationInHours = 12;
		public const long GeofenceExpirationInMillis =	GeofenceExpirationInHours * 60 * 60 * 1000;
		public const float GeofenceRadius = 100;


		public const string ListenConnectionString= "Endpoint=sb://tidbankhackfestnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=rlw2l0/uTUTItrhW3KaWRjvBoQdE+fF5Rx9SjMmsWeE=\n ";
		public const string SenderID = "466754894999";
		public const string NotificationHubName = "TidbankHackfestNotificationHub";


		public static readonly Dictionary<string, LatLng> Geofences = new Dictionary<string, LatLng> {
			{ "hacking_venue", new LatLng (59.9238505,10.7317092) }
		};
	}
}

