using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Thatch.AdministrationService.Data.DbContext;

public class AdministrationServiceDbContextFactory : IDesignTimeDbContextFactory<AdministrationServiceDbContext>
{
    public AdministrationServiceDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(AdministrationServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<AdministrationServiceDbContext>()
            .UseNpgsql(connectionString);

        return new AdministrationServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "Thatch.AdministrationService");

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
