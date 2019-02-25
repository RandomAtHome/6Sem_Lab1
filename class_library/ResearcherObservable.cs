using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library
{
    class ResearcherObservable : System.Collections.ObjectModel.ObservableCollection<Project>
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
        public bool AddDefaultLocalProject()
        {
            Items.Add(new LocalProject());
            return true;
        }
        public bool AddDefaultInternationalProject()
        {
            Items.Add(new InternationalProject());
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

        public ResearcherObservable(string name = "", string surname = "", double experience = 0.0)
        {
            Name = name;
            Surname = surname;
            Experience = experience;
            HasChanged = false;
        }
    }
}
