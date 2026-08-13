using PropertyMaintenanceTriage.Enums;

namespace PropertyMaintenanceTriage.Services
{
    public interface IPriorityService
    {
        Priority DeterminePriority(string? issueDescription);
    }
}
