using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Thatch.DbMigrator.Data;

/* This is used if database provider does't define
 * IThatchDbSchemaMigrator implementation.
 */
public class NullThatchDbSchemaMigrator : IThatchDbSchemaMigrator , ITransientDependency
{
    public Task MigrateAsync()
    {
        throw new NotImplementedException();
    }
}
