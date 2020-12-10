using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EXCSLA.Shared.UI.Blazor.Client.AlertService
{
    public static class AlertServiceConfigurationBuilderExtension
    {
        public static void AddAlertService(this IServiceCollection services)
        {
            services.AddSingleton<IAlertService, AlertService>();
        }
    }
}
