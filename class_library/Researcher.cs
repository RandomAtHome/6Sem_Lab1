using System.Collections;
using System.Collections.Generic;

namespace lab1_v4
{
    public class Researcher : IEnumerable
    {
        public string surname { get; set; }
        public string name { get; set; }
        public double experience { get; set; }
        public List<Project> projects{ get; set; }

        public Researcher(string surname = "", string name = "", double experience = 0.0)
        {
            this.surname = surname;
            this.name = name;
            this.experience = experience;
            this.projects = new List<Project>();
        }

        public void AddProject(params Project[] projects)
        {
            foreach (Project project in projects)
            {
                if (!this.projects.Contains(project))
                {
                    this.projects.Add(project);
                }
            }
            return;
        }

        public void RemoveProjectAt(int index)
        {
            projects.RemoveAt(index);
        }

        public string ToShortString()
        {
            return string.Format("Name: {0} {1}\nYears of experience: {2}", surname, name, experience);
        }

        public override string ToString()
        {
            string result = this.ToShortString() + "\n";
            result += "Projects:\n";
            if (projects.Count == 0)
            {
                result += "No projects!\n";
            }
            foreach (Project project in projects)
            {
                result += project + "\n\n";
            }
            return result;
        }

        public IEnumerator GetEnumerator() => projects.GetEnumerator();

        public IEnumerable GetIntBigProjects()
        {
            foreach (Project i in projects) {
                if (i is InternationalProject && ((InternationalProject)i).participant_count > 2)
                {
                    yield return i;
                }
            }
        }
    }
}
