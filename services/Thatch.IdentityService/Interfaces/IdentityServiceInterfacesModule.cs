using Thatch.IdentityService.Shared;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Thatch.IdentityService.Interfaces;

[DependsOn(
    typeof(IdentityServiceSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule)
    )]
public class IdentityServiceInterfacesModule : AbpModule
{
}
