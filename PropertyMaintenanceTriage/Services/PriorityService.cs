using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Helpers;

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

            if (KeywordMatcher.ContainsAny(issueDescription, UrgentKeywords))
            {
                return Priority.Urgent;
            }

            if (KeywordMatcher.ContainsAny(issueDescription, LowKeywords))
            {
                return Priority.Low;
            }

            return Priority.Medium;
        }
    }
}
