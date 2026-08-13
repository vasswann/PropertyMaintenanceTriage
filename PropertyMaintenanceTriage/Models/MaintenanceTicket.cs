using System.Text.Json.Serialization;

namespace PropertyMaintenanceTriage.Models
{
    public class MaintenanceTicket
    {
        [JsonPropertyName("ticket_id")]
        public string TicketId { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("issue_description")]
        public string IssueDescription { get; set; } = string.Empty;

        [JsonPropertyName("reported_date")]
        public DateOnly ReportedDate { get; set; }
    }
}