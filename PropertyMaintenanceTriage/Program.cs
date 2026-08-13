
using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;

ITicketLoader ticketLoader = new TicketLoader();
IPriorityService priorityService = new PriorityService();
IContractorService contractorService = new ContractorService();

string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "maintenance-tickets.json");

List<MaintenanceTicket> tickets = ticketLoader.LoadFromFile(filePath);

foreach (MaintenanceTicket ticket in tickets)
{
    Priority priority = priorityService.DeterminePriority(ticket.IssueDescription);
    ContractorType contractor = contractorService.DetermineContractor(ticket.IssueDescription);

    Console.WriteLine($"{ticket.TicketId} | {priority} | {contractor}");
}
