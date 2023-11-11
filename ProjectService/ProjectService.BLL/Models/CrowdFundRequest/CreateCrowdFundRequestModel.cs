namespace ProjectService.BLL.Models.CrowdFundRequest;

public class CreateCrowdFundRequestModel
{
    public decimal CrowdFundingAmount { get; set; }
    public string RequestedBy { get; set; } = string.Empty;

    public DateOnly RequestDate { get; set; }
    public DateOnly EndDate { get; set; }
}
