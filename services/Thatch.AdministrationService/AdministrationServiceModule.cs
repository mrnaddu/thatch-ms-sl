using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using StackExchange.Redis;
using System.Collections.Generic;
using Thatch.AdministrationService.Controllers;
using Thatch.AdministrationService.Data;
using Thatch.AdministrationService.Services;
using Thatch.Shared.Hosting;
using Thatch.Shared.Microservices;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Emailing;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Thatch.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceServicesModule),
    typeof(AdministrationServiceControllerModule),
    typeof(AdministrationServiceDataModule),
    typeof(AbpEmailingModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(ThatchSharedMicroservicesModule),
    typeof(ThatchHostingModule)
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

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "AdministrationService:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("AdministrationService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "AdministrationService-Protection-Keys");
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;

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
        //app.UseMultiTenancy(); TODO Erorr DI MultiTenant
        app.UseAuthorization();
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
