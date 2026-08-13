using PropertyMaintenanceTriage.Enums;
using PropertyMaintenanceTriage.Helpers;

namespace PropertyMaintenanceTriage.Services
{
    public class ContractorService : IContractorService
    {
        private static readonly string[] PlumbingKeywords =
        {
            "leak",
            "flood",
            "pipe",
            "toilet",
            "tap",
            "drain"
        };

        private static readonly string[] ElectricalKeywords =
        {
            "lightbulb",
            "light",
            "socket",
            "electrical",
            "sparking",
            "power"
        };

        private static readonly string[] HandymanKeywords =
        {
            "lock",
            "door",
            "handle",
            "cupboard",
            "shelf",
            "hinge"
        };

        public ContractorType DetermineContractor(string? issueDescription)
        {
            if (string.IsNullOrWhiteSpace(issueDescription))
            {
                return ContractorType.PropertyManager;
            }

            if (KeywordMatcher.ContainsAny(issueDescription, PlumbingKeywords))
            {
                return ContractorType.Plumber;
            }

            if (KeywordMatcher.ContainsAny(issueDescription, ElectricalKeywords))
            {
                return ContractorType.Electrician;
            }

            if (KeywordMatcher.ContainsAny(issueDescription, HandymanKeywords))
            {
                return ContractorType.GeneralHandyman;
            }

            return ContractorType.PropertyManager;
        }
    }
}
