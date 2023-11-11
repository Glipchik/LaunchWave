using Mapster;
using ProjectService.BLL.Abstraction.Services;
using ProjectService.BLL.Constants;
using ProjectService.BLL.Exceptions;
using ProjectService.BLL.Models.CrowdFundRequest;
using ProjectService.BLL.Models.Project;
using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Entities;
using ProjectService.Domain.Enums;

namespace ProjectService.BLL.Services;

public class CrowdFundRequestService : GenericService<CrowdFundRequestModel, CrowdFundRequestEntity>, ICrowdFundRequestService
{
    private readonly IProjectService _projectService;

    public CrowdFundRequestService(ICrowdFundRequestRepository repository, IProjectService projectService) : base(repository)
    {
        _projectService = projectService;
    }

    public async Task<CrowdFundRequestModel> Create(CreateCrowdFundRequestModel createModel, CancellationToken ct)
    {
        var requestToCreate = createModel.Adapt<CrowdFundRequestEntity>();
        requestToCreate.Status = CrowdFundRequestStatus.Pending;

        var createdRequest = await Repository.Add(requestToCreate, ct);
        return createdRequest.Adapt<CrowdFundRequestModel>();
    }

    public async Task<CrowdFundRequestModel> RejectCrowdFundRequest(Guid id, CancellationToken ct)
    {
        var request = await Repository.GetById(id, ct);

        if (request is null)
            throw new ModelNotFoundException(nameof(request));

        request.Status = CrowdFundRequestStatus.Rejected;
        var updatedRequest = await Repository.Update(request, ct);

        return updatedRequest.Adapt<CrowdFundRequestModel>();
    }

    public async Task<ProjectModel> AcceptCrowdFundRequest(Guid id, CancellationToken ct)
    {
        var request = await Repository.GetById(id, ct);

        if (request is null)
            throw new ModelNotFoundException(nameof(request));

        if (request.Status is CrowdFundRequestStatus.Rejected)
            throw new InvalidStatusException(ExceptionConstants.RequestRejectedStatus);

        if (request.Status is CrowdFundRequestStatus.Accepted)
        {
            throw new InvalidStatusException(ExceptionConstants.RequestAcceptedStatus);
        }

        if (request.EndDate <= DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ExpiredDateException(ExceptionConstants.EndDateExpired);

        request.Status = CrowdFundRequestStatus.Accepted;
        var updatedRequest = await Repository.Update(request, ct);

        var projectToCreate = new ProjectModel
        {
            CrowdFundingAmount = updatedRequest.CrowdFundingAmount,
            RequestedBy = updatedRequest.RequestedBy,
            CrowdFundRequestId = updatedRequest.Id,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = updatedRequest.EndDate
        };

        var createdProject = await _projectService.Add(projectToCreate, ct);
        return createdProject;
    }
}
