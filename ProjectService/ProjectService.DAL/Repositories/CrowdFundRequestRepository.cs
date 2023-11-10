using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;

namespace ProjectService.DAL.Repositories;

internal class CrowdFundRequestRepository : GenericRepository<CrowdFundRequestEntity>, ICrowdFundRequestRepository
{
    public CrowdFundRequestRepository(DatabaseContext databaseContext, 
        IChangeLogGenericRepository<CrowdFundRequestEntity> changeLogGenericRepository)
        : base(databaseContext, changeLogGenericRepository)
    {
    }
}
