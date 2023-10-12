using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;
using Thatch.TenantService.Localization;
using Volo.Abp.Validation.Localization;

namespace Thatch.TenantService.Shared;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpTenantManagementDomainSharedModule)
)]
public class TenantServiceSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TenantServiceSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TenantServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/TenantService");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("TenantService", typeof(TenantServiceResource));
        });
    }
}
