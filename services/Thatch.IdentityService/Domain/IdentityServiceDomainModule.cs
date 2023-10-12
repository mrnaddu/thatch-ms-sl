using Thatch.IdentityService.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;

namespace Thatch.IdentityService.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityServiceSharedModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule)
)]
public class IdentityServiceDomainModule : AbpModule
{
}
