using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Thatch.TenantService.Interfaces;
using Thatch.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Thatch.TenantService.Controllers;

[DependsOn(
    typeof(TenantServiceInterfacesModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpTenantManagementHttpApiModule)
)]
public class TenantServiceControllerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TenantServiceControllerModule).Assembly);
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TenantServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
