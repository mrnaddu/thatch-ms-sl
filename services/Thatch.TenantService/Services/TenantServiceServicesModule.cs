using Microsoft.Extensions.DependencyInjection;
using Thatch.TenantService.Domain;
using Thatch.TenantService.Interfaces;
using Thatch.TenantService.Services.ObjectMapping;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Thatch.TenantService.Services;

[DependsOn(
    typeof(TenantServiceDomainModule),
    typeof(TenantServiceInterfacesModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementApplicationModule)
    )]
public class TenantServiceServicesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        services.AddAutoMapperObjectMapper<TenantServiceServicesModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TenantServiceServicesModule>(validate: true);
            options.AddProfile<TenantServiceAutoMapperProfile>();
        });
    }
}
