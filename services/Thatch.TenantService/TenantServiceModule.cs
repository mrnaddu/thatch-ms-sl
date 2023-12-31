﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using StackExchange.Redis;
using System.Collections.Generic;
using Thatch.Shared.Hosting;
using Thatch.Shared.Microservices;
using Thatch.TenantService.Controllers;
using Thatch.TenantService.Data;
using Thatch.TenantService.Services;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Thatch.TenantService;

[DependsOn(
    typeof(ThatchHostingModule),
    typeof(TenantServiceServicesModule),
    typeof(TenantServiceDataModule),
    typeof(TenantServiceControllerModule),
    typeof(ThatchSharedMicroservicesModule)
    )]
public class TenantServiceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext  context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        // CongigureJWT 
        JwtBearerConfigurationHelper.Configure(context, "TenantService");

        // ConfigureSwagger
        SwaggerConfigurationHelper.ConfigureWithAuth(
           context: context,
           authority: configuration["AuthServer:Authority"],
           scopes: new Dictionary<string, string> {
                    {"TenantService", "TenantService API"}
                    },
           apiTitle: "TenantService API");

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "TenantService:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("TenantService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "TenantService-Protection-Keys");
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
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TenantService API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("TenantService");
        });
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
