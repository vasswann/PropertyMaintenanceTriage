using PropertyMaintenanceTriage.Cli;
using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;

ITicketLoader ticketLoader = new TicketLoader();
IPriorityService priorityService = new PriorityService();
IContractorService contractorService = new ContractorService();
ITriageService triageService = new TriageService(priorityService, contractorService);

ConsoleUi consoleUi = new ConsoleUi();

List<MaintenanceTicket> tickets = consoleUi.GetTickets(ticketLoader);

List<TriageResult> results = tickets.Select(ticket => triageService.TriageTicket(ticket)).ToList();

consoleUi.DisplayResults(results);