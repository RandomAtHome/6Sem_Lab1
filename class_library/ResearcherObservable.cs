using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace class_library
{
    [Serializable]
    public class ResearcherObservable : System.Collections.ObjectModel.ObservableCollection<Project>
    {
        private bool _hasChanged;

        public string Name { get; set; }
        public string Surname { get; set; }
        public double Experience { get; set; }

        public double InternationalPercent
        {
            get
            {
                return Count != 0
                    ? (double)(from project in this
                       where project is InternationalProject
                       select project).Count() / Count * 100
                    : 0.0;
            }
        }
        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                if (_hasChanged != value)
                {
                    _hasChanged = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("HasChanged"));
                }
            }
        }
        public bool Remove_At(int index)
        {
            try
            {
                RemoveAt(index);
                return true;
            }
            catch (Exception e)
            {
                //do something reasonable
            }
            return false;
        }
        public ResearcherObservable(string name = "", string surname = "", double experience = 0.0)
        {
            CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => {
                HasChanged = true;
                OnPropertyChanged(new PropertyChangedEventArgs("InternationalPercent"));
            };
            Name = name;
            Surname = surname;
            Experience = experience;
            HasChanged = false;
        }

        public ResearcherObservable() : this("Jonh", "Doe", 0.0)
        { }

        public bool AddDefaultLocalProject()
        {
            Add(new LocalProject("Loc. Default"));
            return true;
        }
        public bool AddDefaultInternationalProject()
        {
            Add(new InternationalProject("Int. Default"));
            return true;
        }
        public bool AddCustomInternationalProject(InternationalProject project)
        {
            Add(project);
            return true;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
