using PropertyMaintenanceTriage.Models;
using System.Text.Json;

namespace PropertyMaintenanceTriage.Services
{
    public class TicketLoader : ITicketLoader
    {
        public List<MaintenanceTicket> LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);

            return LoadFromJson(json);
        }

        public List<MaintenanceTicket> LoadFromJson(string json)
        {
            List<MaintenanceTicket>? tickets = JsonSerializer.Deserialize<List<MaintenanceTicket>>(json);

            return tickets ?? new List<MaintenanceTicket>();
        }
    }
}
