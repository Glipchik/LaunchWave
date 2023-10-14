using ProjectService.DAL.Entities.Base;

namespace ProjectService.DAL.Entities;

public class CrowdFundRequestEntity : EntityWithId
{
    public decimal CrowdFundingAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public DateOnly RequestDate { get; set; }
}
