using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PushDemo
{
    /// <summary>
    /// Interface for rest calls.
    /// </summary>
    public interface IDataService
    {
        [Post("/notification/register")]
        Task Register(string deviceId);

        [Post("/notification/begin")]
        Task Begin(string deviceId);

        [Post("/notification/end")]
        Task End(string deviceId);
    }
}
