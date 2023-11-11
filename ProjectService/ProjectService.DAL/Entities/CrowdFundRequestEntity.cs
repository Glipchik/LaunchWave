using ProjectService.DAL.Entities.Base;
using ProjectService.Domain.Enums;

namespace ProjectService.DAL.Entities;

public class CrowdFundRequestEntity : EntityWithId
{
    public decimal CrowdFundingAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;
    public CrowdFundRequestStatus Status { get; set; }

    public DateOnly RequestDate { get; set; }
    public DateOnly EndDate { get; set; }
}
