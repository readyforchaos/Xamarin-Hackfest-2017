using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace PushDemoServer
{
    /// <summary>
    /// Notification Hub handler.
    /// </summary>
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            // Creates hub using full shared connection string and hub name
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://buildpushdemonamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=ZIQNs6AUHtifHJDUe8iykLlJ6wyYNjFQrfs/saLJPQQ=",
                                                                         "PushDemo");
        }
    }
}
