using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Localization;
using Volo.Abp.OpenIddict.Applications;

namespace Thatch.AuthServer.Pages;
public class IndexModel : AbpPageModel
{
    public List<OpenIddictApplication> Applications { get; protected set; }

    public IReadOnlyList<LanguageInfo> Languages { get; protected set; }

    public string CurrentLanguage { get; protected set; }

    protected IOpenIddictApplicationRepository OpenIdApplicationRepository { get; }

    protected ILanguageProvider LanguageProvider { get; }

    public IndexModel(IOpenIddictApplicationRepository openIdApplicationmRepository, ILanguageProvider languageProvider)
    {
        OpenIdApplicationRepository = openIdApplicationmRepository;
        LanguageProvider = languageProvider;
    }

    public ActionResult OnGetAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Redirect("~/Account/Login");
        }          
        else
        {
            Applications =  OpenIdApplicationRepository.GetListAsync().Result;

            Languages =  LanguageProvider.GetLanguagesAsync().Result;
            CurrentLanguage = CultureInfo.CurrentCulture.DisplayName;
            return Page();
        }    
    }
}