using ProjectService.BLL.Models.CrowdFundRequest;

namespace ProjectService.BLL.Models.Project;

public class ProjectModel
{
    public Guid Id { get; set; }
    public decimal CrowdFundingAmount { get; set; }
    public decimal CollectedAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public Guid? CrowdFundRequestId { get; set; }
    public CrowdFundRequestModel? CrowdFundRequest { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
