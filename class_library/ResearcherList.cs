using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1_v4
{
    class ResearcherList
    {
        public DateTime EarliestWWEDate => (from researcher in researchers
                                            from project in researcher.projects
                                            where project is InternationalProject
                                            orderby project.Date ascending
                                            select project.Date).FirstOrDefault();

        public Project EarliestProj => (from researcher in researchers
                                        from project in researcher.projects
                                        orderby project.Date ascending
                                        select project).FirstOrDefault();

        public IEnumerable<LocalProject> LProjectsDecreasingDuration => (from researcher in researchers
                                                                        from project in researcher.projects
                                                                        where project is LocalProject
                                                                        orderby ((LocalProject)project).duration descending
                                                                        select (LocalProject)project).Distinct();

        public IEnumerable<IGrouping<int, InternationalProject>> GroupByParticipantsCount => from project in (from researcher in researchers
                                                                                                              from project in researcher.projects
                                                                                                              where project is InternationalProject
                                                                                                              select project).Distinct()
                                                                                             group (project as InternationalProject) by (project as InternationalProject).participant_count;

        public IEnumerable<Project> RepeatingProjects => from researcher in researchers
                                                         from project in researcher.projects
                                                         group project by project.GetHashCode() into repeating
                                                         where repeating.Count() > 1
                                                         select repeating.First();

        private List<Researcher> researchers;
        public ResearcherList(List<Researcher> researchers = null)
        {
            if (researchers == null)
                researchers = new List<Researcher>();
            this.researchers = researchers;
        }
        public void AddDefaults()
        {
            InternationalProject schoolSlon = new InternationalProject("School Slon", ProjectType.Fundamental, new DateTime(2019, 1, 2), "Russia", 1);
            Researcher first = new Researcher("Chukharev", "Fedor", 2.5);
            first.AddProject(new LocalProject("Hometask", ProjectType.Applied, new DateTime(2018, 11, 6), 10, false));
            first.AddProject(new InternationalProject("Feunman Integrals", ProjectType.Applied, new DateTime(2019, 2, 19), "Russia", 2));
            first.AddProject(schoolSlon);
            researchers.Add(first);

            Researcher second = new Researcher("Polyakov", "Dimitri", 0.1);
            second.AddProject(new InternationalProject("Federal Laws", ProjectType.Applied, new DateTime(2020, 1, 1), "Russia", 2));
            second.AddProject(new InternationalProject("Judgement Systems", ProjectType.Fundamental, new DateTime(2022, 1, 1), "USA", 1));
            second.AddProject(new LocalProject("Taxes", ProjectType.Applied, new DateTime(2018, 11, 1), 3, false));
            second.AddProject(new LocalProject("Hometask", ProjectType.Applied, new DateTime(2018, 11, 6), 1, false));
            researchers.Add(second);

            Researcher third = new Researcher("Fionov", "Alexey", 2);
            third.AddProject(new LocalProject("Japanese lessons", ProjectType.Applied, new DateTime(2018, 10, 30), 2, false));
            third.AddProject(schoolSlon);
            researchers.Add(third);

            Researcher slacker = new Researcher("Dou", "John", 0.0);
            researchers.Add(slacker);
        }
        public override string ToString()
        {
            if (researchers.Count == 0)
            {
                return "List is empty!";
            }
            string result = "";
            string delim = "\n";
            int counter = 1;
            foreach (Researcher researcher in researchers)
            {
                result += counter +": " + researcher + delim;
                counter++;
            }
            return result;
        }
        public override int GetHashCode() => researchers.GetHashCode();
    }
}
