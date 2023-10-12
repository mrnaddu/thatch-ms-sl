using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thatch.TenantService.Interfaces.Sample;
using Thatch.TenantService.Services.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Thatch.TenantService.Controllers.Sample;

[Route("api/TenantService/sample")]
public class SampleController : AbpControllerBase, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService) => _sampleAppService = sampleAppService;

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
