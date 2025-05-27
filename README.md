# Teacher Computer Retrieval - Route Calculator (.NET 8 Console App)

This is a .NET 8 C# console application built to solve a route-based optimization challenge. The application calculates distances, shortest paths, and the number of routes under specific conditions for retrieving teacher computers from various academies.

---

##  Overview

The app accepts user-defined routes in the format `"AB5, BC4, CD8"` and provides answers to predefined queries such as:

1. Calculating distances of specific paths
2. Counting routes with a max or exact number of stops
3. Finding the shortest path between locations
4. Counting routes below a max distance threshold

---

### Architecture

| Layer              | Responsibility                                   |
|---------------     |-------------------------------------------       |
| **Domain**         | Entities, value objects, and interface contracts |
| **Infrastructure** | Implements route logic and parsing               |
| **Application**    | Coordinates input and query execution            |
| **Presentation**   | Console UI                                       |

##  Class Responsibilities

 1. `Route` Class
- Represents a single directed route in the graph.
- Stores:
  - `From` — start node
  - `To` — end node
  - `Distance` — weight of the route

---

 2. `RouteParser` Class
- Parses input like `"AB5, BC4, CD8"` into `Route` objects.
- Skips malformed or duplicate entries.
- Ensures distances are positive.

---

 3. `RouteService` Class
- Implements the **core route calculation logic**.
- Uses a dictionary (`_routeMap`) for fast access to routes from any node.

#### Implemented Methods:

 **`CalculateRouteDistance(string[] path)`**
  - Computes the distance along a specific route like A-B-C.
  - Returns `NO SUCH ROUTE` if a segment doesn't exist.

 **`CountRoutesWithMaxStops(string start, string end, int maxStops)`**
  - Recursively counts all paths between `start` and `end` with at most `maxStops`.

 **`CountRoutesWithExactStops(string start, string end, int exactStops)`**
  - Similar to above, but only counts paths with **exactly** `exactStops`.

 **`FindShortestRouteDistance(string start, string end)`**
  - Uses bounded DFS to compute the shortest path.
  - Handles circular routes (e.g., `B → B`) safely.

 **`CountRoutesWithMaxDistance(string start, string end, int maxDistance)`**
  - Counts all paths with total distance **less than** `maxDistance`.


 4. `IRouteService` Interface
- Defines the contract implemented by `RouteService`.
- Supports abstraction and unit testing.

 5. `RouteQueryRunner` Class
- Executes 8 predefined queries as per the assessment.
- Outputs results in the console via `Console.WriteLine`.

---

 6. `Program.cs` (Main Entry Point)
- Provides a **console-based menu**:

##  Assumptions

1. **Graph is Directed**  
 - AB5 does not imply BA5.

2. **Input Format Must Be Strict**  
 - Format: Two characters + integer (e.g., AB5)
 - Routes like AZ or A5B are skipped.

3. **Duplicate Routes Are Ignored**  
 - Only the first occurrence of a route pair is accepted.

4. **Invalid Paths Are Handled Gracefully**  
 - If a route does not exist, the app displays `NO SUCH ROUTE`.

5. **Recursion Is Bounded**  
 - Shortest path logic includes depth limits to prevent stack overflows on cyclic graphs.

TO RUN:
1- You need VS Code(for mac, windows and Linux) or Visual Studio(for windows)
2. the Project runs on .Net 8, so install .Net 8 SDK

