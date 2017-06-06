using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushDemoServer
{
    /// <summary>
    /// Static "userbase". Implement database/user connection here.
    /// </summary>
    public static class UserBase
    {
        /// <summary>
        /// List of all users in session.
        /// </summary>
        public static List<User> RegisteredUsers = new List<User>();
    }

    public class User
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Total seconds worked.
        /// </summary>
        public int TotalSeconds { get; set; } = 0;

        /// <summary>
        /// Current work-session start time.
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}
