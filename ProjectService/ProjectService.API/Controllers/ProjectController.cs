using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProjectService.API.Constants;
using ProjectService.API.ViewModels.CrowdFundRequest;
using ProjectService.API.ViewModels.Project;
using ProjectService.BLL.Abstraction.Services;
using ProjectService.BLL.Models.CrowdFundRequest;

namespace ProjectService.API.Controllers;

[Route($"api/{ControllerConstants.Projects}")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Get Project by Id
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /Projects/{id}
    /// 
    /// </remarks>
    /// <returns></returns>
    [HttpGet(EndpointConstants.Id)]
    public async Task<ProjectViewModel> Get([FromRoute] Guid id, CancellationToken ct)
    {
        var project = await _projectService.GetById(id, ct);
        return project.Adapt<ProjectViewModel>();
    }

    /// <summary>
    /// Get Projects 
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Projects
    /// 
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<ProjectViewModel>> Get(CancellationToken ct)
    {
        var projects = await _projectService.Get(ct);
        return projects.Adapt<IEnumerable<ProjectViewModel>>();
    }

    /// <summary>
    /// Remove Project
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <param name="ct"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /Projects/{id}
    /// 
    /// </remarks>
    [HttpDelete(EndpointConstants.Id)]
    public async Task Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await _projectService.Delete(id, ct);
    }
}
