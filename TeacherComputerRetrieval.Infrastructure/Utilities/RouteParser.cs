using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrieval.Domain.Entities;

namespace TeacherComputerRetrieval.Infrastructure.Utilities
{
    public static class RouteParser
    {
        public static List<Route> Parse(string input)
        {
            var routes = new List<Route>();
            var seen = new HashSet<string>();

            foreach (var raw in input.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var clean = raw.Trim();
                if (clean.Length < 3) continue;

                string from = clean[0].ToString();
                string to = clean[1].ToString();
                string key = from + to;

                if (seen.Contains(key)) continue;
                seen.Add(key);

                if (int.TryParse(clean[2..], out int distance) && distance > 0)
                    routes.Add(new Route(from, to, distance));
            }

            return routes;
        }
    }
}
