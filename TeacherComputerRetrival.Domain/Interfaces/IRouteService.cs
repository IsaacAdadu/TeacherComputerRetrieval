using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrieval.Domain.ValueObjects;

namespace TeacherComputerRetrieval.Domain.Interfaces
{
    public interface IRouteService
    {
        PathResult CalculateRouteDistance(string[] path);
        int CountRoutesWithMaxStops(string start, string end, int maxStops);
        int CountRoutesWithExactStops(string start, string end, int exactStops);
        int FindShortestRouteDistance(string start, string end);
        int CountRoutesWithMaxDistance(string start, string end, int maxDistance);
    }
}
