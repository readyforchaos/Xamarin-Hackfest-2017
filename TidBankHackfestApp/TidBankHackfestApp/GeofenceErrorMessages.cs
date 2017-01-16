using System;
using Android.Content;
using Android.Gms.Location;

namespace TidBankHackfestApp
{
	public static class GeofenceErrorMessages
	{
		public static string GetErrorString (Context context, int errorCode)
		{
			var mResources = context.Resources;
			switch (errorCode) {
			case GeofenceStatusCodes.GeofenceNotAvailable:
				return "Geofence not available";
			case GeofenceStatusCodes.GeofenceTooManyGeofences:
				return "Too many geofences";
			case GeofenceStatusCodes.GeofenceTooManyPendingIntents:
				return "Too many pending intents";
			default:
				return "Unknown";
			}
		}
	}
}

