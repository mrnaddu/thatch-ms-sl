using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Thatch.AdministrationService.Controllers;
using Thatch.AdministrationService.Data;
using Thatch.AdministrationService.Services;
using Volo.Abp;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Thatch.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceServicesModule),
    typeof(AdministrationServiceControllerModule),
    typeof(AdministrationServiceDataModule),
    typeof(AbpEmailingModule),
    typeof(ThatchHostingModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(ThatchSharedMicroservicesModule)
)]
public class AdministrationServiceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // CongigureJWT 
        JwtBearerConfigurationHelper.Configure(context, "AdministrationService");

        // ConfigureSwagger
        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string> {
                    {"AdministrationService", "AdministrationService API"}
                    },
           apiTitle: "AdministrationService API"
       );

        if (hostingEnvironment.IsDevelopment())
        {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseAbpClaimsMap();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdministrationService API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("AdministrationService");
        });
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
