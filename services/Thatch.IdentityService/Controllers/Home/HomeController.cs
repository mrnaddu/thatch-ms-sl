using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Thatch.IdentityService.Controllers.Home;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");

    }
}
