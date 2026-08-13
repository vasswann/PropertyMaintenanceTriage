using PropertyMaintenanceTriage.Models;

namespace PropertyMaintenanceTriage.Services
{
    public interface ITriageService
    {
        TriageResult TriageTicket(MaintenanceTicket ticket);
    }
}