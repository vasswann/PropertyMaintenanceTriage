using PropertyMaintenanceTriage.Models;

namespace PropertyMaintenanceTriage.Services
{
    public interface ITicketLoader
    {
        List<MaintenanceTicket> LoadFromFile(string filePath);
        List<MaintenanceTicket> LoadFromJson(string json);
    }
}
