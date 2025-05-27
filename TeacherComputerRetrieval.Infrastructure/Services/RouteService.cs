using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrieval.Domain.Entities;
using TeacherComputerRetrieval.Domain.Interfaces;
using TeacherComputerRetrieval.Domain.ValueObjects;

namespace TeacherComputerRetrieval.Infrastructure.Services
{
    public class RouteService : IRouteService
    {

        private static RouteService _lastInstance;

        public static bool LastInstanceExists => _lastInstance != null;
        public static RouteService GetLastInstance() => _lastInstance;

        private readonly Dictionary<string, List<Route>> _routeMap;

        public RouteService(IEnumerable<Route> routes)
        {
            _routeMap = new();
            foreach (var route in routes)
            {
                if (!_routeMap.ContainsKey(route.From))
                    _routeMap[route.From] = new();
                _routeMap[route.From].Add(route);
            }
            _lastInstance = this;
        }

        public PathResult CalculateRouteDistance(string[] path)
        {
            if (path.Length <= 1) return PathResult.NoRoute;

            int total = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                var from = path[i];
                var to = path[i + 1];

                if (!_routeMap.ContainsKey(from)) return PathResult.NoRoute;

                var next = _routeMap[from].FirstOrDefault(r => r.To == to);
                if (next is null) return PathResult.NoRoute;

                total += next.Distance;
            }

            return PathResult.FromDistance(total);
        }

        public int CountRoutesWithMaxStops(string start, string end, int maxStops)
        {
            return CountPaths(start, end, 0, maxStops, (stops, dist) => stops <= maxStops);
        }

        public int CountRoutesWithExactStops(string start, string end, int exactStops)
        {
            return CountPaths(start, end, 0, exactStops, (stops, dist) => stops == exactStops);
        }

        public int FindShortestRouteDistance(string start, string end)
        {
            int minDistance = int.MaxValue;

            // We'll allow cycles, but only explore within a reasonable bound (e.g., < 1000)
            DfsBounded(start, end, 0, ref minDistance, maxDepth: 1000);

            return minDistance == int.MaxValue ? -1 : minDistance;
        }

        public int CountRoutesWithMaxDistance(string start, string end, int maxDistance)
        {
            return CountPaths(start, end, 0, maxDistance, (stops, dist) => dist < maxDistance, limitByDistance: true);
        }

        // -----------------------
        // Internal Helpers
        // -----------------------

        private int CountPaths(string current, string end, int stops, int limit, Func<int, int, bool> predicate, bool limitByDistance = false, int currentDist = 0)
        {
            int count = 0;

            if (predicate(stops, currentDist) && stops > 0 && current == end)
                count++;

            if (!_routeMap.ContainsKey(current))
                return count;

            foreach (var edge in _routeMap[current])
            {
                int nextStops = stops + 1;
                int nextDist = currentDist + edge.Distance;

                if ((limitByDistance && nextDist >= limit) || (!limitByDistance && nextStops > limit))
                    continue;

                count += CountPaths(edge.To, end, nextStops, limit, predicate, limitByDistance, nextDist);
            }

            return count;
        }

        private void DfsBounded(string current, string target, int currentDistance, ref int minDistance, int maxDepth, int depth = 0)
        {
            // Prevent infinite recursion
            if (depth > maxDepth || currentDistance >= minDistance)
                return;

            if (depth > 0 && current == target)
            {
                minDistance = Math.Min(minDistance, currentDistance);
                return;
            }

            if (!_routeMap.ContainsKey(current))
                return;

            foreach (var edge in _routeMap[current])
            {
                DfsBounded(edge.To, target, currentDistance + edge.Distance, ref minDistance, maxDepth, depth + 1);
            }
        }

        
    }
}
