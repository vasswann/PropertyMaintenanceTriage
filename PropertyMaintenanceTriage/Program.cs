
using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;

ITicketLoader ticketLoader = new TicketLoader();

string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "maintenance-tickets.json");

List<MaintenanceTicket> tickets = ticketLoader.LoadFromFile(filePath);

foreach (MaintenanceTicket ticket in tickets)
{
    Console.WriteLine(
        $"{ticket.TicketId} | {ticket.Address} | {ticket.IssueDescription} | {ticket.ReportedDate}"
    );
}
