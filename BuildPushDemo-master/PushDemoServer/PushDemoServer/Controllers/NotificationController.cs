using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PushDemoServer.Controllers
{
    /// <summary>
    /// Controller to handle notification related events.
    /// </summary>
    public class NotificationController : Controller
    {
        /// <summary>
        /// Endpoint for registering a new user.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(string deviceId)
        {
            // Check if user/device already registered
            var user = GetUser(deviceId);
            
            if (user == null)
            {
                // New user
                UserBase.RegisteredUsers.Add(new User
                {
                    DeviceId = deviceId
                });
            }

            return Ok();
        }

        /// <summary>
        /// Endpoint to "begin work". Sets user. BeginTime to be able to calculate work session length on "end work"
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Begin(string deviceId)
        {
            // Get current user
            var user = GetUser(deviceId);

            user.StartTime = DateTime.Now;

            // Create GCM notification payload with title, content and eventKey
            var notif = BuildAndroidNotification("Signed in at work",
                "You started working " + user.StartTime.ToShortTimeString(),
                1);

            // Only send to "current user"
            var tags = new List<string> { user.DeviceId };

            // Send notification
            var outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif, tags);

            if (outcome != null)
            {
                if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                    (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                {
                    // Notification sucessfully sent
                    return Ok();
                }
            }

            return StatusCode(400, "Failed to send notification.");
        }

        /// <summary>
        /// Endpoint to "end work". Calculates time worked from "begin time" and adds to user data.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> End(string deviceId)
        {
            // Get current user
            var user = GetUser(deviceId);

            // Calculate time
            user.TotalSeconds += (int)(DateTime.Now - user.StartTime).TotalSeconds;

            // Create GCM notification payload with title, content and eventKey
            var notif = BuildAndroidNotification("Done for the day", "Total work: " + user.TotalSeconds + "seconds.", 2);
            
            // Only send to "current user"
            var tags = new List<string> { user.DeviceId };

            // Send notification
            var outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif, tags);

            if (outcome != null)
            {
                if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                    (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                {
                    // Notification sucessfully sent
                    return Ok();
                }
            }

            return StatusCode(400, "Failed to send notification.");
        }

        /// <summary>
        /// Local method to retrieve a user from deviceId.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>Returns user or null when not found.</returns>
        private User GetUser(string deviceId)
        {
            var user = UserBase.RegisteredUsers.SingleOrDefault(x => { return x.DeviceId == deviceId; });

            return user;
        }

        /// <summary>
        /// Build GCM notification payload
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private string BuildAndroidNotification(string title, string content, int eventKey = -1)
        {
            return "{ \"data\" : {\"message\":\"" + content + "\", \"title\":\"" + title + "\", \"eventKey\":\"" + eventKey + "\"}}";
        }
    }
}
