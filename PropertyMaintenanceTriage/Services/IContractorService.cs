using PropertyMaintenanceTriage.Enums;

namespace PropertyMaintenanceTriage.Services
{
    public interface IContractorService
    {
        ContractorType DetermineContractor(string? issueDescription);
    }
}
