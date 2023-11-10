using ProjectService.BLL.Abstraction.Services;
using ProjectService.BLL.Models.Project;
using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Entities;

namespace ProjectService.BLL.Services;

public class ProjectService : GenericService<ProjectModel, ProjectEntity>, IProjectService
{
    public ProjectService(IProjectRepository repository) : base(repository)
    {
    }
}
