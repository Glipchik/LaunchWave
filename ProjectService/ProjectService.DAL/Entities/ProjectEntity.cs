using ProjectService.DAL.Entities.Base;

namespace ProjectService.DAL.Entities;

public class ProjectEntity : EntityWithId
{
    public decimal CrowdFundingAmount { get; set; }
    public decimal CollectedAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public Guid? CrowdFundRequestId { get; set; }
    public CrowdFundRequestEntity? CrowdFundRequest { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
