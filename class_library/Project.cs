using System;

namespace class_library
{
    public enum ProjectType { Applied, Fundamental };
    public class Project : IComparable<Project>
    {
        public string Theme { get; set; }
        public ProjectType Type { get; set; }
        public DateTime Date { get; set; }

        public Project(string theme = "", ProjectType type = ProjectType.Applied, DateTime date = new DateTime())
        {
            Theme = theme;
            Type = type;
            Date = date;
	    }

        public override string ToString()
        {
            string delim = "\n";
            return  "Theme: " + Theme + delim +
                    "Type: " + Type + delim +
                    "Ends on: " + Date.ToShortDateString();
        }
        public int CompareTo(Project other) => Date.CompareTo(other.Date);
    }
}