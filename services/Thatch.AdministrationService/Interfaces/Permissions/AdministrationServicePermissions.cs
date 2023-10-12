using Volo.Abp.Reflection;

namespace Thatch.AdministrationService.Interfaces.Permissions;

public class AdministrationServicePermissions
{
    public const string GroupName = "AdministrationService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AdministrationServicePermissions));
    }
}
