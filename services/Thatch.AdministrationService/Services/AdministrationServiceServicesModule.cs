using Microsoft.Extensions.DependencyInjection;
using Thatch.AdministrationService.Domain;
using Thatch.AdministrationService.Interfaces;
using Thatch.AdministrationService.Services.ObjectMapping;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Thatch.AdministrationService.Services;

[DependsOn(
    typeof(AdministrationServiceDomainModule),
    typeof(AdministrationServiceInterfacesModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule)
)]
public class AdministrationServiceServicesModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdministrationServiceServicesModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdministrationServiceServicesModule>(validate: true);
            options.AddProfile<AdministrationServiceAutoMapperProfile>();
        });
    }
}
