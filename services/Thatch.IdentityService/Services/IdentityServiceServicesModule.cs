using Microsoft.Extensions.DependencyInjection;
using Thatch.IdentityService.Domain;
using Thatch.IdentityService.Interfaces;
using Thatch.IdentityService.Services.ObjectMapping;
using Thatch.IdentityService.Shared;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Thatch.IdentityService.Services;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(IdentityServiceDomainModule),
    typeof(IdentityServiceInterfacesModule)
    )]
public class IdentityServiceServicesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<IdentityServiceServicesModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<IdentityServiceServicesModule>(validate: true);
            options.AddProfile<IdentityServiceAutoMapperProfile>();
        });
    }
}
