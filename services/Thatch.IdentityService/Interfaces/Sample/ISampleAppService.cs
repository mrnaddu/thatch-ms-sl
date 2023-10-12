using System.Threading.Tasks;
using Thatch.IdentityService.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Thatch.IdentityService.Interfaces.Sample;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
