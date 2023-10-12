using System.Threading.Tasks;
using Thatch.AdministrationService.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Thatch.AdministrationService.Interfaces.Sample;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
