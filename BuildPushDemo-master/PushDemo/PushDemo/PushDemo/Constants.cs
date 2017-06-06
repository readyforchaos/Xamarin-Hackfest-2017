using System;
using System.Collections.Generic;
using System.Text;

namespace PushDemo
{
    public static class Constants
    {
        // Google API Project Number
        public const string SenderID = "792128146386"; 

        // Azure listen connection string
        public const string ListenConnectionString = "Endpoint=sb://buildpushdemonamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=NFRhlDtO2e6sXp8lzS31whHcN5nFnKuEa9Njkb6FdiA=";

        // Hub name
        public const string NotificationHubName = "PushDemo";
    }
}
