using Thatch.IdentityService.Localization;
using Volo.Abp.Application.Services;

namespace Thatch.IdentityService.Services.ObjectMapping;

public abstract class IdentityServiceAppService : ApplicationService
{
    protected IdentityServiceAppService()
    {
        LocalizationResource = typeof(IdentityServiceResource);
        ObjectMapperContext = typeof(IdentityServiceServicesModule);
    }
}
