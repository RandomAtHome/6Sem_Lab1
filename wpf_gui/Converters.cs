using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using class_library;

namespace wpf_gui
{
    [ValueConversion(typeof(Project), typeof(string))]
    public class ProjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Project proj = (Project)value;
            return (value is InternationalProject ? "International" : "Local") + " project: " + proj.Theme;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(LocalProject), typeof(string))]
    public class LocalProjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LocalProject proj = (LocalProject)value;
            return proj.Type + " " + proj.Theme;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
