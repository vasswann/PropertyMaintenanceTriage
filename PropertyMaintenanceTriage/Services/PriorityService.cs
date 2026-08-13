using PropertyMaintenanceTriage.Enums;

namespace PropertyMaintenanceTriage.Services
{
    public class PriorityService : IPriorityService
    {
        private static readonly string[] UrgentKeywords =
       {
            "leak",
            "flood",
            "gas",
            "burst pipe",
            "sparking",
            "smoke"
        };

        private static readonly string[] LowKeywords =
        {
            "lightbulb",
            "loose handle",
            "paint",
            "cosmetic"
        };

        public Priority DeterminePriority(string? issueDescription)
        {
            if (string.IsNullOrWhiteSpace(issueDescription))
            {
                return Priority.Medium;
            }

            if (ContainsAnyKeyword(issueDescription, UrgentKeywords))
            {
                return Priority.Urgent;
            }

            if (ContainsAnyKeyword(issueDescription, LowKeywords))
            {
                return Priority.Low;
            }

            return Priority.Medium;
        }

        private static bool ContainsAnyKeyword(string description, string[] keywords)
        {
            return keywords.Any(keyword => description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}
