using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Thatch.TenantService.Interfaces.Sample;
using Thatch.TenantService.Services.Dtos;
using Volo.Abp.Application.Services;

namespace Thatch.TenantService.Services.Sample;
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
