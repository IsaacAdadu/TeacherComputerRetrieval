using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherComputerRetrieval.Domain.Entities
{
    public class Route
    {
        public string From { get; }
        public string To { get; }
        public int Distance { get; }

        public Route(string from, string to, int distance)
        {
            From = from;
            To = to;
            Distance = distance;
        }
    }
}
