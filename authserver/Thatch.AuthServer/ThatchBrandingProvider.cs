using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Thatch.AuthServer;

[Dependency(ReplaceServices = true)]
public class ThatchBrandingProvider : DefaultBrandingProvider
{
    private readonly ICurrentTenant _currentTenant;
    public ThatchBrandingProvider(ICurrentTenant currentTenant) => _currentTenant = currentTenant;
    public override string AppName => _currentTenant.Name ?? "Thatch";
}
