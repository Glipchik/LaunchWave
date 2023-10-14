using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;

namespace ProjectService.DAL.Repositories;

internal class CrowdFundRequestEntityRepository : GenericRepository<CrowdFundRequestEntity>, ICrowdFundRequestEntityRepository
{
    public CrowdFundRequestEntityRepository(DatabaseContext databaseContext, 
        IChangeLogGenericRepository<CrowdFundRequestEntity> changeLogGenericRepository)
        : base(databaseContext, changeLogGenericRepository)
    {
    }
}
