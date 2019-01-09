using iAttend.Student.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Services
{
    class Connectivity : IConnectivity
    {
        public bool IsConnected =>
            Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;
    }
}
