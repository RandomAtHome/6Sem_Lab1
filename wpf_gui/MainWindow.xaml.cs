using System;
using System.Collections.Generic;
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
            Researcher = new ResearcherObservable("James", "Hopkins", 1.0);
            this.DataContext = Researcher;
            Researcher.AddDefaultInternationalProject();
            Researcher.AddDefaultLocalProject();
            Researcher.AddDefaultInternationalProject();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(allProjectsLBox is null) && this.TryFindResource("key_project_DataTemplate") is DataTemplate dataTemplate)
            {
                if (allProjectsLBox.ItemTemplate == null)
                    allProjectsLBox.ItemTemplate = dataTemplate;
                else
                    allProjectsLBox.ItemTemplate = null;
            }
        }
    }
}
