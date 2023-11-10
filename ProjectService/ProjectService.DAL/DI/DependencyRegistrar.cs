using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectService.DAL.Abstraction.Repositories;
using ProjectService.DAL.Constants;
using ProjectService.DAL.Contexts;
using ProjectService.DAL.Entities;
using ProjectService.DAL.Interceptors;
using ProjectService.DAL.Repositories;

namespace ProjectService.DAL.DI;

public static class DependencyRegistrar
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DatabaseContext>(context =>
        {
            context.UseNpgsql(config.GetConnectionString(DIConstants.DefaultConnection));
            //.AddInterceptors(new SoftDeleteInterceptor());
        });

        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<ICrowdFundRequestRepository, CrowdFundRequestRepository>();
        services.AddTransient<IGenericRepository<ChangeLogEntity>, GenericRepository<ChangeLogEntity>>();

        services.AddTransient(typeof(IChangeLogGenericRepository<>), typeof(ChangeLogGenericRepository<>));

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
