using BlazorStrap;
using EXCSLA.Shared.Core.ValueObjects.Common;
using EXCSLA.Shared.UI.Blazor.Client.AlertService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Blazor.Client
{
    public partial class AlertBox
    {
        [Inject] IAlertService AlertService { get; set; }

        protected List<Alert> Alerts { get; set; } = new List<Alert>();
        [Parameter] public Color Color { get; set; } = Color.Info;

        protected override void OnInitialized()
        {
            AlertService.OnAlert += (o, s) =>
            {
                this.Alerts.Add(s);
                StateHasChanged();
                Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((t) =>
                {
                    this.Alerts.Remove(s);
                    StateHasChanged();
                });
            };
        }

        protected Color GetColorFromAlertType(AlertType alertType)
        {
            Color returnColor;
            switch (alertType)
            {
                case AlertType.Primary:
                    returnColor = Color.Primary;
                    break;
                case AlertType.Secondary:
                    returnColor = Color.Secondary;
                    break;
                case AlertType.Success:
                    returnColor = Color.Success;
                    break;
                case AlertType.Danger:
                    returnColor = Color.Danger;
                    break;
                case AlertType.Warning:
                    returnColor = Color.Warning;
                    break;
                case AlertType.Info:
                    returnColor = Color.Info;
                    break;
                case AlertType.Light:
                    returnColor = Color.Light;
                    break;
                case AlertType.Dark:
                    returnColor = Color.Dark;
                    break;
                default:
                    returnColor = Color.Primary;
                    break;
            }

            return returnColor;
        }
    }
}
