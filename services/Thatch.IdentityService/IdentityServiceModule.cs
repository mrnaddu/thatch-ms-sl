using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using StackExchange.Redis;
using System.Collections.Generic;
using Thatch.IdentityService.Controllers;
using Thatch.IdentityService.Data;
using Thatch.IdentityService.Services;
using Thatch.Shared.Hosting;
using Thatch.Shared.Microservices;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Thatch.IdentityService;

[DependsOn(
    typeof(IdentityServiceControllerModule),
    typeof(IdentityServiceServicesModule),
    typeof(IdentityServiceDataModule),
    typeof(ThatchHostingModule),
    typeof(ThatchSharedMicroservicesModule)
)]
public class IdentityServiceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // CongigureJWT 
        JwtBearerConfigurationHelper.Configure(context, "IdentityService");

        // ConfigureSwagger
        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string> {
                    {"IdentityService", "IdentityService API"}
                    },
           apiTitle: "IdentityService API");

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "IdentityService:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("IdentityService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "IdentityService-Protection-Keys");
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
        app.UseAuthorization();
        //app.UseMultiTenancy(); TODO Erorr DI MultiTenant
        app.UseAbpClaimsMap();
        app.UseSwagger();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ThatchService API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("IdentityService");
        });
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
