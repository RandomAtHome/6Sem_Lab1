using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using class_library;

namespace wpf_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResearcherObservable researcher = null;

        public MainWindow()
        {
            InitializeComponent();
            //ListCollectionView listCollectionView = Resources["key_local_project_view"] as ListCollectionView;
            //listCollectionView.Filter = item => { return item != null && item is LocalProject; };
            //LocalListBox.ItemsSource = listCollectionView;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(allProjectsLBox is null) && this.TryFindResource("key_project_DataTemplate") is DataTemplate dataTemplate)
            {
                allProjectsLBox.ItemTemplate = allProjectsLBox.ItemTemplate == null ? dataTemplate : null;
            }
        }

        void IsLocalProject(object sender, FilterEventArgs args)
        {
            if (args.Item is Project proj)
            {
                args.Accepted = proj is LocalProject ? true : false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            researcher.AddDefaultLocalProject();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs args)
        {
            if (args.Item is Project proj)
            {
                args.Accepted = proj is InternationalProject ? true : false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            researcher = FindResource("key_MainDataSource") as ResearcherObservable;
            CountryPicker.DataContext = GetListOfCountries();
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultLocalProject();
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultLocalProject();
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultLocalProject();
            researcher.AddDefaultInternationalProject();
        }

        private List<string> GetListOfCountries()
        {
            List<string> result = new List<string>();
            foreach (var country in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo region = new RegionInfo(country.LCID);
                if (!result.Contains(region.DisplayName))
                {
                    result.Add(region.DisplayName);
                }
            }
            result.Sort();
            return result;
        }

        private void AddNewIntProject(object sender, RoutedEventArgs e)
        {
            InternationalProject proj_ref = FindResource("key_DummyProject") as InternationalProject;
            foreach (FrameworkElement child in NewProjDataInput.Children)
            {
                if (Validation.GetHasError(child))
                {
                    return;
                }
            }
            researcher.AddCustomInternationalProject(new InternationalProject(proj_ref));
        }

        private void CheckInputNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
