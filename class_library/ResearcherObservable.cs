using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library
{
    public class ResearcherObservable : System.Collections.ObjectModel.ObservableCollection<Project>, INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Experience { get; set; }
        public double InternationalPercent {
            get {
                return Items.Count != 0
                    ? (from project in Items
                     where project is InternationalProject
                     select project).Count() / Items.Count * 100
                    : 0.0;
            }
        }
        public bool HasChanged { get; set; }
        public bool Remove_At(int index)
        {
            try {
                Items.RemoveAt(index);
                return true;
            } catch (Exception e){
                //do something reasonable
            }
            return false;
        }
        public ResearcherObservable(string name = "", string surname = "", double experience = 0.0) 
        {
            Name = name;
            Surname = surname;
            Experience = experience;
            HasChanged = false;
        }

        public ResearcherObservable() : this("", "", 0.0)
        {}

        public bool AddDefaultLocalProject()
        {
            Items.Add(new LocalProject("Loc. Default"));
            return true;
        }
        public bool AddDefaultInternationalProject()
        {
            Items.Add(new InternationalProject("Int. Default"));
            return true;
        }
        public bool AddCustomInternationalProject(InternationalProject project)
        {
            Items.Add(project);
            return true;
        }
        public override string ToString()
        {
            throw new NotImplementedException();
            return base.ToString();
        }
    }
}
