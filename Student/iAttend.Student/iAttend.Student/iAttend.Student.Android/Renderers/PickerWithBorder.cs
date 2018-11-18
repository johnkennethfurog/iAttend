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
using iAttend.Student.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(iAttend.Student.Renderers.DatePickerWithBorder), typeof(DatePickerWithBorder))]
namespace iAttend.Student.Droid.Renderers
{
    class DatePickerWithBorder : DatePickerRenderer
    {
        public DatePickerWithBorder(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            this.Control.Background = null;

        }
    }
}