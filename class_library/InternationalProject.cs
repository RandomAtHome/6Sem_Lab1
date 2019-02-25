using System;

namespace class_library
{
    public class InternationalProject : Project, IComparable<InternationalProject>
    {
        public string country_host { set; get; }
        public int participant_count { set; get; }
        public InternationalProject(string theme = "", ProjectType type = ProjectType.Applied, DateTime date = new DateTime(), string country_host = "", int participant_count = 0) :
            base(theme, type, date)
        {
            this.country_host = country_host;
            this.participant_count = participant_count;
        }
        public override string ToString()
        {
            string delim = "\n";
            return base.ToString() + delim + "Country host: " + country_host + delim + "Total participants: " + participant_count;
        }

        int IComparable<InternationalProject>.CompareTo(InternationalProject other) => participant_count - other.participant_count;
    }
}
