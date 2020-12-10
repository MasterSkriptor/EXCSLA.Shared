using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EXCSLA.Shared.UI.Blazor.Client.HttpApiClient
{
    public static class HttpApiClientRequestBuilderFactoryServiceExtensions
    {
        public static void AddApiClientRequestBuilder(this IServiceCollection services)
        {
            services.AddScoped<IHttpApiClientRequestBuilderFactory, HttpApiClientRequestBuilderFactory>();
        }
    }
}
