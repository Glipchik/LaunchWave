using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProjectService.API.Constants;
using ProjectService.API.ViewModels.CrowdFundRequest;
using ProjectService.API.ViewModels.Project;
using ProjectService.BLL.Abstraction.Services;
using ProjectService.BLL.Models.CrowdFundRequest;

namespace ProjectService.API.Controllers;

[Route($"api/{ControllerConstants.CrowdFundRequests}")]
[ApiController]
public class CrowdFundRequestController : ControllerBase
{
    private readonly ICrowdFundRequestService _crowdFundRequestService;
    private readonly IValidator<CreateCrowdFundRequestViewModel> _createValidator;

    public CrowdFundRequestController(ICrowdFundRequestService crowdFundRequestService, IValidator<CreateCrowdFundRequestViewModel> createValidator)
    {
        _crowdFundRequestService = crowdFundRequestService;
        _createValidator = createValidator;
    }

    /// <summary>
    /// Create crowd fund request
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /CrowdFundRequests
    ///     {
    ///         "crowdFundingAmount": 0,
    ///         "requestedBy": "string",
    ///         "requestDate": "2023-01-01",
    ///         "endDate": "2023-01-01"
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    public async Task<CrowdFundRequestViewModel> Create(CreateCrowdFundRequestViewModel crowdFundRequestViewModel, CancellationToken ct)
    {
        await _createValidator.ValidateAndThrowAsync(crowdFundRequestViewModel, ct);

        var request = await _crowdFundRequestService.Create(crowdFundRequestViewModel.Adapt<CreateCrowdFundRequestModel>(), ct);
        return request.Adapt<CrowdFundRequestViewModel>();
    }

    /// <summary>
    /// Get Crowd Fund Request by Id
    /// </summary>
    /// <param name="id">CrowdFundRequest Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /CrowdFundRequests/{id}
    /// 
    /// </remarks>
    /// <returns></returns>
    [HttpGet(EndpointConstants.Id)]
    public async Task<CrowdFundRequestViewModel> Get([FromRoute] Guid id, CancellationToken ct)
    {
        var crowdFundRequest = await _crowdFundRequestService.GetById(id, ct);
        return crowdFundRequest.Adapt<CrowdFundRequestViewModel>();
    }

    /// <summary>
    /// Get Crowd Fund Requests 
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /CrowdFundRequests
    /// 
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<CrowdFundRequestViewModel>> Get(CancellationToken ct)
    {
        var crowdFundRequests = await _crowdFundRequestService.Get(ct);
        return crowdFundRequests.Adapt<IEnumerable<CrowdFundRequestViewModel>>();
    }

    /// <summary>
    /// Remove Crowd Fund Request
    /// </summary>
    /// <param name="id">Crowd Fund Request Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /CrowdFundRequests/{id}
    /// 
    /// </remarks>
    [HttpDelete(EndpointConstants.Id)]
    public async Task Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await _crowdFundRequestService.Delete(id, ct);
    }

    /// <summary>
    /// Reject crowd fund request
    /// </summary>
    /// <param name="id">Crowd Fund Request Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PATCH /CrowdFundRequests/{id}
    /// 
    /// </remarks>
    [HttpPatch($"{EndpointConstants.Reject}/{EndpointConstants.Id}")]
    public async Task<CrowdFundRequestViewModel> RejectRequest([FromRoute] Guid id, CancellationToken ct)
    {
        return (await _crowdFundRequestService.RejectCrowdFundRequest(id, ct)).Adapt<CrowdFundRequestViewModel>();
    }

    /// <summary>
    /// Accept crowd fund request
    /// </summary>
    /// <param name="id">Crowd Fund Request Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PATCH /CrowdFundRequests/{id}
    /// 
    /// </remarks>
    [HttpPatch($"{EndpointConstants.Accept}/{EndpointConstants.Id}")]
    public async Task<ProjectViewModel> AcceptRequest([FromRoute] Guid id, CancellationToken ct)
    {
        return (await _crowdFundRequestService.AcceptCrowdFundRequest(id, ct)).Adapt<ProjectViewModel>();
    }
}
