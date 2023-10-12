using Microsoft.Extensions.DependencyInjection;
using System;
using Thatch.TenantService.Data.DbContext;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Thatch.TenantService.Data;

[DependsOn(
    typeof(TenantServiceDomainModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TenantServiceDataModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        services.AddAbpDbContext<TenantServiceDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also TenantServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseNpgsql();
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        context.Services.AddAbpDbContext<TenantServiceDbContext>(options =>
        {
            options.ReplaceDbContext<ITenantManagementDbContext>();

            options.AddDefaultRepositories(true);
        });
    }
}
