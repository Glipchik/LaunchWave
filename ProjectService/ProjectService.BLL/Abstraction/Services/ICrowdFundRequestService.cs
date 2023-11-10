using ProjectService.BLL.Models.CrowdFundRequest;
using ProjectService.BLL.Models.Project;

namespace ProjectService.BLL.Abstraction.Services;

public interface ICrowdFundRequestService : IGenericService<CrowdFundRequestModel>
{
    Task<CrowdFundRequestModel> Create(CreateCrowdFundRequestModel createModel, CancellationToken ct);

    Task<CrowdFundRequestModel> RejectCrowdFundRequest(Guid id, CancellationToken ct);

    Task<ProjectModel> AcceptCrowdFundRequest(Guid id, CancellationToken ct);
}
