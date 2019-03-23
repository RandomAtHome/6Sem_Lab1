using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public ResearcherObservable Researcher { get; set; }

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
            Project proj = args.Item as Project;
            if (proj != null)
            {
                args.Accepted = proj is LocalProject ? true : false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Researcher.AddDefaultLocalProject();
            //(Resources["key_local_project_view"] as CollectionViewSource).View.Refresh();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs args)
        {
            Project proj = args.Item as Project;
            if (proj != null)
            {
                args.Accepted = proj is InternationalProject ? true : false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Researcher = FindResource("key_MainDataSource") as ResearcherObservable;
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultLocalProject();
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultLocalProject();
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultLocalProject();
            Researcher.AddDefaultInternationalProject();
        }
    }
}
