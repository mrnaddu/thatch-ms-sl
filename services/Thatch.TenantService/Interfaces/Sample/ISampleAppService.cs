using System.Threading.Tasks;
using Thatch.TenantService.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Thatch.TenantService.Interfaces.Sample;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
