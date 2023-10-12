using Thatch.IdentityService.Data;
using Thatch.IdentityService.Interfaces;
using Thatch.ThatchService.Data;
using Thatch.ThatchService.Interfaces;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Thatch.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ThatchServiceDataModule),
    typeof(ThatchServiceInterfacesModule),
    typeof(IdentityServiceDataModule),
    typeof(IdentityServiceInterfacesModule)
)]
public class ThatchDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
