using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;

namespace ProjectService.DAL.Repositories;

internal class ProjectRepository : GenericRepository<ProjectEntity>, IProjectRepository
{
    public ProjectRepository(DatabaseContext databaseContext,
        IChangeLogGenericRepository<ProjectEntity> changeLogGenericRepository)
        : base(databaseContext, changeLogGenericRepository)
    {
    }
}
