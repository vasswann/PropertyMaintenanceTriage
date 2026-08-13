using PropertyMaintenanceTriage.Models;

namespace PropertyMaintenanceTriage.Services
{
    public class TriageService : ITriageService
    {
        private readonly IPriorityService _priorityService;
        private readonly IContractorService _contractorService;

        public TriageService(IPriorityService priorityService, IContractorService contractorService)
        {
            _priorityService = priorityService;
            _contractorService = contractorService;
        }

        public TriageResult TriageTicket(MaintenanceTicket ticket)
        {
            return new TriageResult
            {
                TicketId = ticket.TicketId,
                Address = ticket.Address,
                Priority = _priorityService.DeterminePriority(ticket.IssueDescription),
                AssignedContractor = _contractorService.DetermineContractor(ticket.IssueDescription)
            };
        }
    }
}
