using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Thatch.AdministrationService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace Thatch.AdministrationService.Controllers;

[DependsOn(
    typeof(AdministrationServiceInterfacesModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule)
)]
public class AdministrationServiceControllerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AdministrationServiceControllerModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AdministrationServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
