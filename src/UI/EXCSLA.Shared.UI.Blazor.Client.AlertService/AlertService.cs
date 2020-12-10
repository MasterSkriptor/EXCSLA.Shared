using EXCSLA.Shared.Core.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EXCSLA.Shared.UI.Blazor.Client.AlertService
{
    public class AlertService : IAlertService
    {
        public event EventHandler<Alert> OnAlert;

        public List<Alert> Alerts { get; set; }

        public void AddAlert(string message) => AddAlert(message, AlertType.Info);
        public void AddAlert(string message, AlertType type) => OnAlert.Invoke(this, new Alert(message, type));

    }
}
