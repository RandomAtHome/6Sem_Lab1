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
        ResearcherObservable researcher;
        public ResearcherObservable Researcher { get => researcher; set => researcher = value; }

        public MainWindow()
        {
            InitializeComponent();
            researcher = new ResearcherObservable("James", "Hopkins", 1.0);
            this.DataContext = researcher;
            researcher.AddDefaultInternationalProject();
            researcher.AddDefaultLocalProject();
            researcher.AddDefaultInternationalProject();
        }
    }
}
