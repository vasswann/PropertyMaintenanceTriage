using PropertyMaintenanceTriage.Services;
using System.Text.Json;

namespace PropertyMaintenanceTriage.Tests.Services
{
    public class TicketLoaderTests
    {
        private TicketLoader _ticketLoader;

        [SetUp]
        public void Setup()
        {
            _ticketLoader = new TicketLoader();
        }

        [Test]
        public void LoadFromJson_WhenJsonIsValid_ReturnsTickets()
        {
            // Arrange
            string json = """
                [
                    {
                        "ticket_id": "TKT-101",
                        "address": "Flat 4, 12 High Street",
                        "issue_description": "There is a water leak.",
                        "reported_date": "2026-07-21"
                    }
                ]
                """;

            // Act
            var result = _ticketLoader.LoadFromJson(json);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].TicketId, Is.EqualTo("TKT-101"));
            Assert.That(result[0].Address, Is.EqualTo("Flat 4, 12 High Street"));
            Assert.That(result[0].IssueDescription, Is.EqualTo("There is a water leak."));
            Assert.That(result[0].ReportedDate, Is.EqualTo(new DateOnly(2026, 7, 21)));
        }

        [Test]
        public void LoadFromJson_WhenJsonArrayIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string json = "[]";

            // Act
            var result = _ticketLoader.LoadFromJson(json);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void LoadFromJson_WhenJsonIsNull_ReturnsEmptyList()
        {
            // Arrange
            string json = "null";

            // Act
            var result = _ticketLoader.LoadFromJson(json);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void LoadFromJson_WhenJsonIsInvalid_ThrowsJsonException()
        {
            // Arrange
            string json = "This is not valid JSON";

            // Act & Assert
            Assert.Throws<JsonException>(() => _ticketLoader.LoadFromJson(json));
        }
    }
}