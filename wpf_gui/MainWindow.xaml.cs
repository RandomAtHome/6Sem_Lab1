using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using Microsoft.Win32;

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
            //this.DataContext = researcher;
            CountryPicker.DataContext = GetListOfCountries();
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

        private void AddCustomIntProject_Click(object sender, RoutedEventArgs e)
        {
            ThemeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            ParticipantCount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
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

        private void AddDefaultLocal_Click(object sender, RoutedEventArgs e) => researcher.AddDefaultLocalProject();

        private void AddDefaultIntProject_Click(object sender, RoutedEventArgs e) => researcher.AddDefaultInternationalProject();

        private void ResearcherFieldChanged(object sender, TextChangedEventArgs e)
        {
            if (researcher != null)
            {
                researcher.HasChanged = true;
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            researcher.Remove_At(allProjectsLBox.SelectedIndex);
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {

            if (researcher.HasChanged)
            {
                MessageBoxResult res = MessageBox.Show("Do you want to save changes?", "Warning", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    SaveClick(this, null);
                }
            }
            Resources["key_MainDataSource"] = new ResearcherObservable();
            researcher = FindResource("key_MainDataSource") as ResearcherObservable;
        }


        public bool Save(string filename)
        {
            FileStream fs = null;
            bool res = false;
            try
            {
                fs = File.Create(filename);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, researcher);
                res = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return res;
        }

        public bool Load(string filename)
        {
            bool result = false;
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(filename);
                BinaryFormatter bf = new BinaryFormatter();
                Resources["key_MainDataSource"] = bf.Deserialize(fs) as ResearcherObservable;
                researcher = FindResource("key_MainDataSource") as ResearcherObservable;
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return result;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dg = new SaveFileDialog();
            if (dg.ShowDialog() == true)
            {
                try
                {
                    researcher.HasChanged = false;
                    Save(dg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to save file!");
                }
            }
            researcher.HasChanged = false;
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            if (researcher.HasChanged == false)
            {
                OpenFile();
                return;
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Do you want to save changes?", "Warning", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    SaveClick(this, null);
                }
                OpenFile();
            }
        }
        private void OpenFile()
        {
            OpenFileDialog dg = new OpenFileDialog();
            if (dg.ShowDialog() == true)
            {
                try
                {
                    Load(dg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to open file!");
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (researcher.HasChanged)
            {
                MessageBoxResult res = MessageBox.Show("Do you want to save changes?", "Warning", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    SaveClick(this, null);
                }
            }
        }
    };
}
