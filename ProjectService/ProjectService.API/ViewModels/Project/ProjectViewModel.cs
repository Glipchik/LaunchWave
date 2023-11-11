using ProjectService.API.ViewModels.CrowdFundRequest;

namespace ProjectService.API.ViewModels.Project;

public class ProjectViewModel
{
    public Guid Id { get; set; }
    public decimal CrowdFundingAmount { get; set; }
    public decimal CollectedAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public Guid? CrowdFundRequestId { get; set; }
    public CrowdFundRequestViewModel? CrowdFundRequest { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
