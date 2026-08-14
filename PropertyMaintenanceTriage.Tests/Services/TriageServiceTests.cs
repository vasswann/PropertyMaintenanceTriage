using Moq;
using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Models;
using PropertyMaintenanceTriage.Services;

namespace PropertyMaintenanceTriage.Tests.Services
{
    public class TriageServiceTests
    {
        private Mock<IPriorityService> _priorityServiceMock;
        private Mock<IContractorService> _contractorServiceMock;
        private TriageService _triageService;

        [SetUp]
        public void Setup()
        {
            _priorityServiceMock = new Mock<IPriorityService>();
            _contractorServiceMock = new Mock<IContractorService>();
            _triageService = new TriageService(_priorityServiceMock.Object, _contractorServiceMock.Object);
        }

        [Test]
        public void TriageTicket_WhenTicketIsProvided_ReturnsExpectedTriageResult()
        {
            // Arrange
            MaintenanceTicket ticket = new MaintenanceTicket
            {
                TicketId = "TKT-101",
                Address = "Flat 4, 12 High Street",
                IssueDescription = "There is a massive water leak.",
                ReportedDate = new DateOnly(2026, 7, 21)
            };

            _priorityServiceMock.Setup(x => x.DeterminePriority(ticket.IssueDescription)).Returns(Priority.Urgent);
            _contractorServiceMock.Setup(x => x.DetermineContractor(ticket.IssueDescription)).Returns(ContractorType.Plumber);

            // Act
            TriageResult result = _triageService.TriageTicket(ticket);

            // Assert
            Assert.That(result.TicketId, Is.EqualTo(ticket.TicketId));
            Assert.That(result.Address, Is.EqualTo(ticket.Address));
            Assert.That(result.Priority, Is.EqualTo(Priority.Urgent));
            Assert.That(result.AssignedContractor, Is.EqualTo(ContractorType.Plumber));
        }

        [Test]
        public void TriageTicket_CallsPriorityAndContractorServicesOnce()
        {
            // Arrange
            MaintenanceTicket ticket = new MaintenanceTicket
            {
                TicketId = "TKT-102",
                Address = "Apt 12, Victoria Court",
                IssueDescription = "The kitchen lightbulb needs replacing.",
                ReportedDate = new DateOnly(2026, 7, 21)
            };

            _priorityServiceMock.Setup(x => x.DeterminePriority(ticket.IssueDescription)).Returns(Priority.Low);
            _contractorServiceMock.Setup(x => x.DetermineContractor(ticket.IssueDescription)).Returns(ContractorType.Electrician);

            // Act
            _triageService.TriageTicket(ticket);

            // Assert
            _priorityServiceMock.Verify(x => x.DeterminePriority(ticket.IssueDescription), Times.Once);
            _contractorServiceMock.Verify(x => x.DetermineContractor(ticket.IssueDescription), Times.Once);
        }
    }
}