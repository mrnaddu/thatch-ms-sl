using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Thatch.IdentityService.Interfaces;
using Thatch.IdentityService.Localization;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Thatch.IdentityService.Controllers;

[DependsOn(
    typeof(IdentityServiceInterfacesModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule)
    )]
public class IdentityServiceControllerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(IdentityServiceControllerModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<IdentityServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
