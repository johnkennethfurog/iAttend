using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iAttend.Student.Renderers
{
    public class DatePickerWithBorder : DatePicker
    {
        public bool WithBorder { get; set; }

        public bool AlignRight { get; set; }
    }
}
