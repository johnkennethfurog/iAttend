using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iAttend.Student.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(iAttend.Student.Droid.DependencyServices.MessageService))]
namespace iAttend.Student.Droid.DependencyServices
{
    class MessageService : IMessageService
    {
        public void ShowMessage(string message, bool durationIsLong = false)
        {
            Toast.MakeText(Android.App.Application.Context, message, durationIsLong ? ToastLength.Long : ToastLength.Short).Show();
        }
    }
}