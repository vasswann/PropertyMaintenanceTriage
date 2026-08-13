
using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;

ITicketLoader ticketLoader = new TicketLoader();
IPriorityService priorityService = new PriorityService();
IContractorService contractorService = new ContractorService();

ITriageService triageService = new TriageService(priorityService, contractorService);

string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "maintenance-tickets.json");

List<MaintenanceTicket> tickets = ticketLoader.LoadFromFile(filePath);

foreach (MaintenanceTicket ticket in tickets)
{
    TriageResult result = triageService.TriageTicket(ticket);

    Console.WriteLine(
        $"{result.TicketId} | {result.Address} | {result.AssignedContractor} | {result.Priority}"
    );
}
