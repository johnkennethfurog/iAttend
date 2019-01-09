using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.DependencyServices
{
    public interface IWifiConnector
    {
        Task<bool> ConnectToWifi(string ssid, string password);
        void Disconnect();
    }
}
