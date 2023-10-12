using System.Threading.Tasks;

namespace Thatch.DbMigrator.Data;

public interface IThatchDbSchemaMigrator
{
    Task MigrateAsync();
}
