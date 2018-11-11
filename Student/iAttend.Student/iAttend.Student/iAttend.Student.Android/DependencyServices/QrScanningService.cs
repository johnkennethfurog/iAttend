using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iAttend.Student.DependencyServices;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(iAttend.Student.Droid.DependencyServices.QrScanningService))]
namespace iAttend.Student.Droid.DependencyServices
{
    class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var scanner = new MobileBarcodeScanner();

            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                UseNativeScanning = true,
                PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.QR_CODE }
            };

            var result = await scanner.Scan(Android.App.Application.Context,optionsCustom);

            return result?.Text;
        }
    }
}