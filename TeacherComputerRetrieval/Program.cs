// See https://aka.ms/new-console-template for more information
using TeacherComputerRetrieval.Application.Queries;
using TeacherComputerRetrieval.Infrastructure.Services;
using TeacherComputerRetrieval.Infrastructure.Utilities;

Console.WriteLine("Hello, World!");

Console.WriteLine("Enter routes (e.g., AB5, BC4, CD8):");
string input = Console.ReadLine();

var routes = RouteParser.Parse(input);
var service = new RouteService(routes);
var runner = new RouteQueryRunner(service);

runner.Run();

