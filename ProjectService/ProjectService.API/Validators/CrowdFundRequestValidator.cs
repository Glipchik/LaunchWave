using FluentValidation;
using ProjectService.API.ViewModels.CrowdFundRequest;

namespace ProjectService.API.Validators;

public class CreateCrowdFundRequestViewModelValidator : AbstractValidator<CreateCrowdFundRequestViewModel>
{
    public CreateCrowdFundRequestViewModelValidator()
    {
        RuleFor(r => r.CrowdFundingAmount).NotEmpty().GreaterThan(0);
        RuleFor(r => r.RequestDate).NotEmpty();
        RuleFor(r => r.RequestedBy).NotEmpty();
        RuleFor(r => r.EndDate).Must(d => d > DateOnly.FromDateTime(DateTime.UtcNow));
    }
}
