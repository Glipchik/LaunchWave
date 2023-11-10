namespace ProjectService.API.ViewModels.CrowdFundRequest;

public class CreateCrowdFundRequestViewModel
{
    public decimal CrowdFundingAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public DateOnly RequestDate { get; set; }
    public DateOnly EndDate { get; set; }
}
