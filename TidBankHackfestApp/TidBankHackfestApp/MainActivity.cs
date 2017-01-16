using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Gms.Common.Apis;
using System.Collections.Generic;
using Android.Gms.Location;
using Android.Media;
using Android.Util;
using Java.Lang;
using Gcm.Client;

namespace TidBankHackfestApp
{
    [Activity(Label = "TidBankHackfestApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity,
		GoogleApiClient.IConnectionCallbacks,
		GoogleApiClient.IOnConnectionFailedListener
	{
		protected const string TAG = "creating-and-monitoring-geofences";
		protected GoogleApiClient mGoogleApiClient;
		protected IList<IGeofence> mGeofenceList = new List<IGeofence>();

		public static MainActivity instance;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			instance = this;

			mGeofenceList = new List<IGeofence>();
			PopulateGeofenceList();
			BuildGoogleApiClient();
			RegisterGCM();
		}

		void RegisterGCM()
		{
			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);

			GcmClient.Register(this, Constants.SenderID);
		}

		protected void BuildGoogleApiClient()
		{
			mGoogleApiClient = new GoogleApiClient.Builder(this)
				.AddConnectionCallbacks(this)
				.AddOnConnectionFailedListener(this)
				.AddApi(LocationServices.API)
				.Build();
		}

		protected override void OnStart()
		{
			base.OnStart();
			mGoogleApiClient.Connect();

		}

		protected override void OnStop()
		{
			base.OnStop();
			mGoogleApiClient.Disconnect();
		}

		public void OnConnected(Bundle connectionHint)
		{
			Log.Info(TAG, "Connected to GoogleApiClient");

			 var status = LocationServices.GeofencingApi.AddGeofencesAsync(mGoogleApiClient, GetGeofencingRequest(mGeofenceList),
			   GetGeofencePendingIntent());
		}

		public void OnConnectionSuspended(int cause)
		{
			Log.Info(TAG, "Connection suspended");
		}

		public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
		{
			Log.Info(TAG, "Connection failed: ConnectionResult.getErrorCode() = " + result.ErrorCode);
		}

		void LogSecurityException(SecurityException securityException)
		{
			Log.Error(TAG, "Invalid location permission. " +
				"You need to use ACCESS_FINE_LOCATION with geofences", securityException);
		}

		PendingIntent GetGeofencePendingIntent()
		{
			var intent = new Intent(this, typeof(GeofenceTransitionsIntentService));
			return PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
		}

		public void PopulateGeofenceList()
		{
			foreach (var entry in Constants.Geofences)
			{
				mGeofenceList.Add(new GeofenceBuilder()
					.SetRequestId(entry.Key)
					.SetCircularRegion(
						entry.Value.Latitude,
						entry.Value.Longitude,
						Constants.GeofenceRadius
					)
					.SetExpirationDuration(Constants.GeofenceExpirationInMillis)
					.SetTransitionTypes(Geofence.GeofenceTransitionEnter |
						Geofence.GeofenceTransitionExit)
				                  .Build());
			}
		}

		GeofencingRequest GetGeofencingRequest(IList<IGeofence> geofence)
		{
			var builder = new GeofencingRequest.Builder();
			builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);
			builder.AddGeofences(geofence);

			return builder.Build();
		}


	}
}

