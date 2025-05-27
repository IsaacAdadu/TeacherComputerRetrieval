using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherComputerRetrieval.Domain.ValueObjects
{
    public record PathResult(bool Success, int Distance)
    {
        public static PathResult NoRoute => new(false, -1);
        public static PathResult FromDistance(int distance) => new(true, distance);
    }
}
