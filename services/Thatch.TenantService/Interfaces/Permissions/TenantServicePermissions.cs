using Volo.Abp.Reflection;

namespace Thatch.TenantService.Interfaces.Permissions;

public class TenantServicePermissions
{
    public const string GroupName = "TenantService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TenantServicePermissions));
    }
}
