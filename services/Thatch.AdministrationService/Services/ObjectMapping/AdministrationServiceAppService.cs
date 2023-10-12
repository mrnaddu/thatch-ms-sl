using Thatch.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace Thatch.AdministrationService.Services.ObjectMapping;

public class AdministrationServiceAppService : ApplicationService
{
    public AdministrationServiceAppService()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
        ObjectMapperContext = typeof(AdministrationServiceModule);
    }
}
