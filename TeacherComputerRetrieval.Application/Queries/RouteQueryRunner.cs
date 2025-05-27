using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrieval.Domain.Interfaces;

namespace TeacherComputerRetrieval.Application.Queries
{
    public class RouteQueryRunner
    {
        private readonly IRouteService _service;

        public RouteQueryRunner(IRouteService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("1. " + FormatResult(_service.CalculateRouteDistance(new[] { "A", "B", "C" })));
            Console.WriteLine("2. " + FormatResult(_service.CalculateRouteDistance(new[] { "A", "E", "B", "C", "D" })));
            Console.WriteLine("3. " + FormatResult(_service.CalculateRouteDistance(new[] { "A", "E", "D" })));
            Console.WriteLine("4. " + _service.CountRoutesWithMaxStops("C", "C", 3));
            Console.WriteLine("5. " + _service.CountRoutesWithExactStops("A", "C", 4));
            Console.WriteLine("6. " + _service.FindShortestRouteDistance("A", "C"));
            Console.WriteLine("7. " + _service.FindShortestRouteDistance("B", "B"));
            Console.WriteLine("8. " + _service.CountRoutesWithMaxDistance("C", "C", 30));
        }

        private string FormatResult(Domain.ValueObjects.PathResult result)
            => result.Success ? result.Distance.ToString() : "NO SUCH ROUTE";
    }
}
