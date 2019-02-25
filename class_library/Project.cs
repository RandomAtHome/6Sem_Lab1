using System;

namespace lab1_v4
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
        public override int GetHashCode() => Date.GetHashCode() + Theme.GetHashCode() + (int)Type;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Date.Equals((obj as Project).Date) && Theme.Equals((obj as Project).Theme) && Type.Equals((obj as Project).Type); 
        }

        public int CompareTo(Project other) => Date.CompareTo(other.Date);

        public static bool operator ==(Project a, Project b) => ReferenceEquals(a, b) || (!(a is null) && a.Equals(b));
        public static bool operator !=(Project a, Project b) => !(a == b);
    }
}