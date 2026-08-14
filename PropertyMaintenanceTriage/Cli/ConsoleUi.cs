using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;
using System.Text.Json;

namespace PropertyMaintenanceTriage.Cli
{
    public class ConsoleUi
    {
        public List<MaintenanceTicket> GetTickets(ITicketLoader ticketLoader)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(" Property Maintenance Triage System");
            Console.WriteLine("========================================");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an input source:");
                Console.WriteLine("1. Use sample JSON file");
                Console.WriteLine("2. Paste JSON manually");
                Console.WriteLine();
                Console.Write("Enter selection: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "maintenance-tickets.json");

                    return ticketLoader.LoadFromFile(filePath);
                }

                if (choice == "2")
                {
                    return ReadTicketsFromConsole(ticketLoader);
                }

                Console.WriteLine();
                Console.WriteLine("Invalid selection. Please enter 1 or 2.");
            }
        }

        public void DisplayResults(List<TriageResult> results)
        {
            Console.WriteLine();
            Console.WriteLine($"{"Ticket ID",-12} {"Address",-30} {"Contractor",-20} {"Priority",-10}");

            Console.WriteLine(new string('-', 75));

            foreach (TriageResult result in results)
            {
                Console.WriteLine(
                    $"{result.TicketId,-12} " +
                    $"{result.Address,-30} " +
                    $"{result.AssignedContractor,-20} " +
                    $"{result.Priority,-10}");
            }

            Console.WriteLine();
            Console.WriteLine($"{results.Count} ticket(s) processed.");
        }

        private List<MaintenanceTicket> ReadTicketsFromConsole(ITicketLoader ticketLoader)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Paste the JSON below.");
                Console.WriteLine("The input must be a JSON array of ticket objects.");
                Console.WriteLine("Type END on a new line when finished.");
                Console.WriteLine();

                List<string> lines = new List<string>();

                while (true)
                {
                    string? line = Console.ReadLine();

                    if (line is null || line.Equals("END", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    lines.Add(line);
                }

                string json = string.Join(Environment.NewLine, lines);

                try
                {
                    return ticketLoader.LoadFromJson(json);
                }
                catch (JsonException)
                {
                    ShowInvalidJsonMessage();
                }
            }
        }

        private static void ShowInvalidJsonMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid JSON input.");
            Console.WriteLine("Please provide a JSON array of maintenance ticket objects.");
            Console.WriteLine("Please try again.");
        }
    }
}