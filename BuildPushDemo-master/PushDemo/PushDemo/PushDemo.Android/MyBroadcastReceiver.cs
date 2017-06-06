using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;
using System;
using Refit;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is needed only for Android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace PushDemo.Droid
{

    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { Constants.SenderID };
    }

    [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }

        public PushHandlerService() : base(Constants.SenderID)
        {

        }

        /// <summary>
        /// Called when sucessfully registered with GCM.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="registrationId"></param>
        protected override void OnRegistered(Context context, string registrationId)
        {
            MainPage.deviceId = Android.OS.Build.Serial;

            RegistrationID = registrationId;
            
            dialogNotify("Success!", "Registered device with GCM");

            Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
                                        context);
            // Remove all existing Hub registrations
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Add listen tags. deviceId = user identity
            var tags = new List<string> { MainPage.deviceId };

            // Register to Hub
            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // REST call to register with backend
            var restAPI = RestService.For<IDataService>("http://buildpushdemo.azurewebsites.net");
            restAPI.Register(MainPage.deviceId);

        }

        /// <summary>
        /// Called when a new GCM message is recieved.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="oldintent"></param>
        protected override void OnMessage(Context context, Intent oldintent)
        {
            var msg = new StringBuilder();

            // Grab message data.
            string titleText = oldintent.Extras.GetString("title");
            string messageText = oldintent.Extras.GetString("message");
            int eventKey = Convert.ToInt32(oldintent.Extras.GetString("eventKey"));

            // Handle notification event. -1 default
            HandleNotificationEvent(eventKey);

            // Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            // Add intent to resume app on notification click
            Intent intent = new Intent(context, typeof(MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 1, intent, PendingIntentFlags.UpdateCurrent);

            // Create the notification
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, titleText);

            // Auto-cancel will remove the notification once the user touches it
            notification.Flags = NotificationFlags.AutoCancel;

            // Set the notification info
            // we use the pending intent, passing our ui intent over, which will get called
            // when the notification is tapped.
            notification.SetLatestEventInfo(MainActivity.instance, titleText, messageText, pendingIntent);

            // Show the notification
            // eventKey as notification id
            notificationManager.Notify(eventKey, notification);
        }

        /// <summary>
        /// Implement business logic based on eventKey
        /// </summary>
        /// <param name="eventKey"></param>
        private void HandleNotificationEvent(int eventKey)
        {
            // Event specific code:
            switch (eventKey)
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// Display popup with title and message.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        protected void dialogNotify(String title, String message)
        {

            MainActivity.instance.RunOnUiThread(() => {
                AlertDialog.Builder dlg = new AlertDialog.Builder(MainActivity.instance);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(title);
                alert.SetButton("Ok", delegate {
                    alert.Dismiss();
                });
                alert.SetMessage(message);
                alert.Show();
            });
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            dialogNotify("GCM Unregistered...", "The device has been unregistered!");
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Console.WriteLine("Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Console.WriteLine("GCM Error: " + errorId);
        }
    }
}