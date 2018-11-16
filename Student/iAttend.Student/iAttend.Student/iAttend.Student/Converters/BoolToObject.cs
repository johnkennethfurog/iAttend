using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace iAttend.Student.Converters
{
    class IsPresentToImageTransformationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPresent = (bool)value;

            var transformations = new List<ITransformation>();
            if (!isPresent)
                 transformations.Add(new FFImageLoading.Transformations.GrayscaleTransformation());

            return transformations;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IsPresentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPresent = (bool)value;
            return isPresent ? Color.Green : Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IsPresentToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPresent = (bool)value;

            return isPresent ? "Present" : "Absent";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
