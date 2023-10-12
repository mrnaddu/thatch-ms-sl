using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Volo.Abp.Modularity;

namespace Thatch.Shared.Hosting;

public static class JwtBearerConfigurationHelper
{
    public static void Configure(ServiceConfigurationContext context, string audience)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.Authority = configuration["AuthServer:Authority"];
                            options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                            options.Audience = audience;
                            options.BackchannelHttpHandler = new HttpClientHandler
                            {
                                ServerCertificateCustomValidationCallback =
                              (message, certificate, chain, sslPolicyErrors) => true
                            };
                        });
    }
}
