using ProjectService.Domain.Enums;

namespace ProjectService.API.ViewModels.CrowdFundRequest;

public class CrowdFundRequestViewModel
{
    public Guid Id { get; set; }
    public decimal CrowdFundingAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;
    public CrowdFundRequestStatus Status { get; set; }

    public DateOnly RequestDate { get; set; }
    public DateOnly EndDate { get; set; }
}
