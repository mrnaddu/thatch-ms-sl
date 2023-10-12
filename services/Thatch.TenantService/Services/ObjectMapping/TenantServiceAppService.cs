using Thatch.TenantService.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace Thatch.TenantService.Services.ObjectMapping;

public abstract class TenantServiceAppService : ApplicationService
{
    protected TenantServiceAppService()
    {
        LocalizationResource = typeof(TenantServiceResource);
        ObjectMapperContext = typeof(TenantServiceServicesModule);
    }
}
