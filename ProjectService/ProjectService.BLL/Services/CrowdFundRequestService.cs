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
    private readonly IChangeLogGenericRepository<CrowdFundRequestEntity> _changeLogRepository;

    public CrowdFundRequestService(ICrowdFundRequestRepository repository, IProjectService projectService,
        IChangeLogGenericRepository<CrowdFundRequestEntity> changeLogRepository) : base(repository)
    {
        _projectService = projectService;
        _changeLogRepository = changeLogRepository;
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

        var changeLog = new ChangeLogEntity()
        {
            EntityName = typeof(CrowdFundRequestModel).ToString(),
            PrimaryKeyValue = id.ToString(),
            PropertyName = "Status",
            OldValue = request.Status.ToString(),
            NewValue = CrowdFundRequestStatus.Rejected.ToString()
        };

        request.Status = CrowdFundRequestStatus.Rejected;
        var updatedRequest = await _changeLogRepository.Update(request, changeLog, ct);

        return updatedRequest.Adapt<CrowdFundRequestModel>();
    }

    public async Task<ProjectModel> AcceptCrowdFundRequest(Guid id, CancellationToken ct)
    {
        var request = await Repository.GetById(id, ct);

        ValidateAcceptingRequest(request);

        var changeLog = new ChangeLogEntity()
        {
            EntityName = typeof(CrowdFundRequestModel).ToString(),
            PrimaryKeyValue = id.ToString(),
            PropertyName = "Status",
            OldValue = request!.Status.ToString(),
            NewValue = CrowdFundRequestStatus.Accepted.ToString()
        };

        request.Status = CrowdFundRequestStatus.Accepted;
        var updatedRequest = await _changeLogRepository.Update(request, changeLog, ct);

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

    private static void ValidateAcceptingRequest(CrowdFundRequestEntity? request)
    {
        if (request is null)
            throw new ModelNotFoundException(nameof(request));

        switch (request.Status)
        {
            case CrowdFundRequestStatus.Rejected:
                throw new InvalidStatusException(ExceptionConstants.RequestRejectedStatus);
            case CrowdFundRequestStatus.Accepted:
                throw new InvalidStatusException(ExceptionConstants.RequestAcceptedStatus);
        }

        if (request.EndDate <= DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ExpiredDateException(ExceptionConstants.EndDateExpired);
    }
}
