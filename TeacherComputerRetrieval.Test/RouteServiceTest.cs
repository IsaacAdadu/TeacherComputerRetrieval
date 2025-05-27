using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherComputerRetrieval.Domain.Entities;
using TeacherComputerRetrieval.Infrastructure.Services;
using Xunit;

namespace TeacherComputerRetrieval.Test
{
    public class RouteServiceTest
    {
        private readonly RouteService _service;

        public RouteServiceTest()
        {
            var routes = new List<Route>
            {
                new("A", "B", 5),
                new("B", "C", 4),
                new("C", "D", 8),
                new("D", "C", 8),
                new("D", "E", 6),
                new("A", "D", 5),
                new("C", "E", 2),
                new("E", "B", 3),
                new("A", "E", 7),
            };

            _service = new RouteService(routes);
        }

        [Fact]
        public void Test_CalculateRouteDistance_ABC()
        {
            var result = _service.CalculateRouteDistance(new[] { "A", "B", "C" });
            Assert.True(result.Success);
            Assert.Equal(9, result.Distance);
        }

        [Fact]
        public void Test_CalculateRouteDistance_AEBCD()
        {
            var result = _service.CalculateRouteDistance(new[] { "A", "E", "B", "C", "D" });
            Assert.True(result.Success);
            Assert.Equal(22, result.Distance);
        }

        [Fact]
        public void Test_CalculateRouteDistance_AED_Invalid()
        {
            var result = _service.CalculateRouteDistance(new[] { "A", "E", "D" });
            Assert.False(result.Success);
        }

        [Fact]
        public void Test_CountRoutes_CtoC_Max3Stops()
        {
            var count = _service.CountRoutesWithMaxStops("C", "C", 3);
            Assert.Equal(2, count);
        }

        [Fact]
        public void Test_CountRoutes_AtoC_Exact4Stops()
        {
            var count = _service.CountRoutesWithExactStops("A", "C", 4);
            Assert.Equal(3, count);
        }

        [Fact]
        public void Test_ShortestRoute_AtoC()
        {
            var result = _service.FindShortestRouteDistance("A", "C");
            Assert.Equal(9, result);
        }

        [Fact]
        public void Test_ShortestRoute_BtoB()
        {
            var result = _service.FindShortestRouteDistance("B", "B");
            Assert.Equal(9, result);
        }

        [Fact]
        public void Test_CountRoutes_CtoC_MaxDistance30()
        {
            var count = _service.CountRoutesWithMaxDistance("C", "C", 30);
            Assert.Equal(7, count);
        }
    }
}

