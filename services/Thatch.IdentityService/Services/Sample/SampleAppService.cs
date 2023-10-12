using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Thatch.IdentityService.Interfaces.Sample;
using Thatch.IdentityService.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Thatch.IdentityService.Services.Sample;
public class SampleAppService : ApplicationService, ISampleAppService
{
    public Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }
}
