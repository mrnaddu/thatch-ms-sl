using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore.Design;
using Thatch.TenantService.Shared.Const;

namespace Thatch.TenantService.Data.DbContext;

public class TenantServiceDbContextFactory : IDesignTimeDbContextFactory<TenantServiceDbContext>
{
    public TenantServiceDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(TenantServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<TenantServiceDbContext>()
            .UseNpgsql(connectionString);

        return new TenantServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
