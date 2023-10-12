using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thatch.IdentityService.Interfaces.Sample;
using Thatch.IdentityService.Services.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Thatch.IdentityService.Controllers;
 
[Route("api/IdentityService/sample")]
public class SampleController : AbpControllerBase, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)=> _sampleAppService = sampleAppService;

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
