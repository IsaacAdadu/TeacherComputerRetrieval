// See https://aka.ms/new-console-template for more information

using TeacherComputerRetrieval.Application.Queries;
using TeacherComputerRetrieval.Infrastructure.Services;
using TeacherComputerRetrieval.Infrastructure.Utilities;

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Teacher Computer Retrieval Route Calculator ===");
    Console.WriteLine("1. Enter new route map");
    Console.WriteLine("2. Run predefined queries on last entered route map");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }

    if (choice == "1")
    {
        Console.Clear();
        Console.Write("Enter routes (e.g., AB5, BC4, CD8): ");
        string input = Console.ReadLine();

        var routes = RouteParser.Parse(input);

        if (routes == null || routes.Count == 0)
        {
            Console.WriteLine("❌ No valid routes parsed. Please try again.");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
            continue;
        }

        var service = new RouteService(routes);
        var runner = new RouteQueryRunner(service);

        Console.Clear();
        Console.WriteLine("✔ Routes successfully loaded.");
       // Console.WriteLine("\nRunning predefined queries on the entered map...\n");

        runner.Run();

        Console.WriteLine("\n✔ Done. Press any key to return to the menu...");
        Console.ReadKey();
    }
    else if (choice == "2")
    {
        if (!RouteService.LastInstanceExists)
        {
            Console.WriteLine("⚠ Please enter a route map first (option 1).");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
            continue;
        }

        Console.Clear();
        Console.WriteLine("Running test queries...\n");

        var runner = new RouteQueryRunner(RouteService.GetLastInstance());
        runner.Run();

        Console.WriteLine("\n✔ Done. Press any key to return to menu...");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("❌ Invalid option. Press any key to try again.");
        Console.ReadKey();
    }
}

