using Thatch.TenantService.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Thatch.TenantService.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TenantServiceSharedModule),
    typeof(AbpTenantManagementDomainModule)
)]
public class TenantServiceDomainModule : AbpModule
{

}
