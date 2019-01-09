using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iAttend.Student.DependencyServices;

namespace iAttend.Student.Droid.DependencyServices
{
    class WifiConnector : IWifiConnector
    {
        private readonly WifiManager _wifiManager;

        private bool _isWifiEnabled;
        private int _currentWifiConnectionId;

        public WifiConnector()
        {
            _wifiManager = (WifiManager)Android.App.Application.Context
                                   .GetSystemService(Context.WifiService);
        }

        public Task<bool> ConnectToWifi(string ssid, string password)
        {

            var formattedSsid = $"\"{ssid}\"";
            var formattedPassword = $"\"{password}\"";

            var wifiConfig = new WifiConfiguration
            {
                Ssid = formattedSsid,
                PreSharedKey = formattedPassword
            };

            _isWifiEnabled = _wifiManager.IsWifiEnabled;
            _currentWifiConnectionId = _wifiManager.ConnectionInfo.NetworkId;

            var network = _wifiManager.ConfiguredNetworks
                 .FirstOrDefault(n => n.Ssid == formattedSsid);

            if (network == null)
            {
                var addNetwork = _wifiManager.AddNetwork(wifiConfig);
                if (network == null)
                {
                    Console.WriteLine($"Cannot connect to network: {ssid}");
                    return Task.FromResult(false);
                }
            }
            
            _wifiManager.Disconnect();

            if (_wifiManager.WifiState == Android.Net.WifiState.Disabled)
                _wifiManager.SetWifiEnabled(true);

            var enableNetwork = _wifiManager.EnableNetwork(network.NetworkId, true);
            
            return Task.FromResult(enableNetwork);
        }

        public void Disconnect()
        {
            if (!_isWifiEnabled)
            {
                _wifiManager.Disconnect();
                _wifiManager.SetWifiEnabled(false);
            }
            else
            {
                if (_wifiManager.ConnectionInfo.NetworkId != _currentWifiConnectionId)
                {
                    _wifiManager.Disconnect();
                    _wifiManager.EnableNetwork(_currentWifiConnectionId, true);
                }
            }
        }
    }
}