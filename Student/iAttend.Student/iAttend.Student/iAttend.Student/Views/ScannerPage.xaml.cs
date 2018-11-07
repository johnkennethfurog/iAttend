using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace iAttend.Student.Views
{
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            scanner.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            scanner.IsScanning = false;
        }

        private void Scanner_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                scanner.IsAnalyzing = false;
                await DisplayAlert("Scanned result", result.Text, "OK");
            });
        }
    }
}
