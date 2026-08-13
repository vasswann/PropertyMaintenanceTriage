using PropertyMaintenanceTriage.Enums;

namespace PropertyMaintenanceTriage.Models
{
    public class TriageResult
    {
        public string TicketId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public ContractorType AssignedContractor { get; set; }
    }
}
