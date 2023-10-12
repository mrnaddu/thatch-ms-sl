using Thatch.TenantService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Thatch.TenantService.Interfaces.Permissions;

public class TenantServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TenantServicePermissions.GroupName, L("Permission:TenantService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TenantServiceResource>(name);
    }
}
