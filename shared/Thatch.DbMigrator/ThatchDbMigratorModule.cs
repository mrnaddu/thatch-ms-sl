using Thatch.AdministrationService.Data;
using Thatch.AdministrationService.Interfaces;
using Thatch.IdentityService.Data;
using Thatch.IdentityService.Interfaces;
using Thatch.TenantService.Data;
using Thatch.TenantService.Interfaces;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Thatch.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationServiceDataModule),
    typeof(AdministrationServiceInterfacesModule),
    typeof(IdentityServiceDataModule),
    typeof(IdentityServiceInterfacesModule),
    typeof(TenantServiceDataModule),
    typeof(TenantServiceInterfacesModule)
)]
public class ThatchDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
