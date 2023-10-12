using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Thatch.TenantService.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TenantServiceDomainSharedModule),
    typeof(AbpTenantManagementDomainModule)
)]
public class TenantServiceDomainModule : AbpModule
{

}
