using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thatch.AdministrationService.Data.DbContext;
using Thatch.IdentityService.Data;
using Thatch.TenantService.Data.DbContext;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace Thatch.DbMigrator.Data;

public class ThatchDbMigrationService : ITransientDependency
{
    private readonly IDataSeeder _dataSeeder;
    private readonly ILogger<ThatchDbMigrationService> _logger;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;

    public ThatchDbMigrationService(
        IDataSeeder dataSeeder,
        ILogger<ThatchDbMigrationService> logger,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant)
    {
        _dataSeeder = dataSeeder;
        _logger = logger;
        _unitOfWorkManager = unitOfWorkManager;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started database migrations...");

        await MigrateHostAsync(cancellationToken);
        await MigrateTenantsAsync(cancellationToken);
        _logger.LogInformation("Migration completed!");
    }

    private async Task MigrateHostAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrating Host side...");
        await MigrateDatabaseSchemaAsync(null, cancellationToken);
        await SeedAdminAsync();

        _logger.LogInformation($"Successfully completed host database migrations.");
    }

    public async Task MigrateTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

        var migratedDatabaseSchemas = new HashSet<string>();
        foreach (var tenant in tenants)
        {
            using (_currentTenant.Change(tenant.Id))
            {
                if (tenant.ConnectionStrings.Any())
                {
                    var tenantConnectionStrings = tenant.ConnectionStrings
                        .Select(x => x.Value)
                        .ToList();

                    if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                    {
                        _logger.LogInformation($"Migrating tenant database: {tenant.Name} ({tenant.Id})");
                        await MigrateDatabaseSchemaAsync(tenant, cancellationToken);

                        migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                    }
                }

                _logger.LogInformation($"Seeding tenant data: {tenant.Name} ({tenant.Id})");
                await SeedAdminAsync(tenant);
            }

            _logger.LogInformation($"Successfully completed {tenant.Name} tenant database migrations.");
        }

        _logger.LogInformation("Successfully completed all database migrations.");
        _logger.LogInformation("You can safely end this process...");
    }

    private async Task MigrateDatabaseSchemaAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Migrating schema for {(tenant == null ? "host" : tenant.Name + " tenant")} database...");

        using (var uow = _unitOfWorkManager.Begin(true))
        {
            if (tenant == null)
            {
                await MigrateDatabaseAsync<TenantServiceDbContext>(cancellationToken);
            }
            await MigrateDatabaseAsync<IdentityServiceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<AdministrationServiceDbContext>(cancellationToken);
            await uow.CompleteAsync(cancellationToken);
        }
    }

    private async Task MigrateDatabaseAsync<TDbContext>(
        CancellationToken cancellationToken) 
        where TDbContext : DbContext, IEfCoreDbContext
    {
        _logger.LogInformation($"Migrating {typeof(TDbContext).Name.RemovePostFix("DbContext")} database...");

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext.Database.MigrateAsync(cancellationToken);
    }

    private async Task SeedAdminAsync(Tenant tenant = null)
    {
        _logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");

        await _dataSeeder.SeedAsync(
            new DataSeedContext()
                .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
                .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
        );
    }
}
