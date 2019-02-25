using System;

namespace lab1_v4
{
    public class LocalProject : Project
    {
        public double duration { set; get; }
        public bool government_funds { set; get; }
        public LocalProject(string theme = "", ProjectType type = ProjectType.Applied, DateTime date = new DateTime(), double duration = 0.0, bool government_funds = false) :
            base(theme, type, date)
        {
            this.duration = duration;
            this.government_funds = government_funds;
        }
        public override string ToString()
        {
            string delim = "\n";
            return base.ToString() + delim + "Duration: " + duration + delim + "Is funded by government: " + government_funds;
        }
    }
}
