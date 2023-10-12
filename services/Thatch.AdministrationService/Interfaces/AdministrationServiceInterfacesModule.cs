using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Thatch.AdministrationService.Interfaces;

[DependsOn(
    typeof(AdministrationServiceSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule)
)]
public class AdministrationServiceInterfacesModule : AbpModule
{
}
