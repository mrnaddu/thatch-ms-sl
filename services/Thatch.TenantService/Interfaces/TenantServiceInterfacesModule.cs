using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Thatch.TenantService.Interfaces;

[DependsOn(
    typeof(TenantServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpTenantManagementApplicationContractsModule)
)]
public class TenantServiceInterfacesModule
{
}
