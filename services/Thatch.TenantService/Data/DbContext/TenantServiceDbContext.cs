using Microsoft.EntityFrameworkCore;
using Thatch.TenantService.Shared.Const;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Thatch.TenantService.Data.DbContext;


[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public class TenantServiceDbContext : AbpDbContext<TenantServiceDbContext>,
    ITenantManagementDbContext
{
    public TenantServiceDbContext(DbContextOptions<TenantServiceDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantManagement();
    }
}
