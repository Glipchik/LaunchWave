using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectService.BLL.Abstraction.Services;
using ProjectService.BLL.Services;
using ProjectService.DAL.DI;

namespace ProjectService.BLL.DI;

public static class DependencyRegistrar
{
    public static void AddBusinessDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataAccessDependencies(config);
        AddServices(services);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IProjectService, Services.ProjectService>();
        services.AddTransient<ICrowdFundRequestService, CrowdFundRequestService>();
    }
}
