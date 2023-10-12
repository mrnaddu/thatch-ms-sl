using Volo.Abp.Reflection;

namespace Thatch.IdentityService.Interfaces.Permissions;

public class IdentityServicePermissions
{
    public const string GroupName = "IdentityService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityServicePermissions));
    }
}
