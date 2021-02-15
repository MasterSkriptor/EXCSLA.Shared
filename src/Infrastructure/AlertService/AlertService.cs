using EXCSLA.Shared.Core.Interfaces;
using EXCSLA.Shared.Core.ValueObjects.Common;
using System.Collections.Generic;
using System.Linq;

namespace EXCSLA.Shared.Infrastructure.Services
{
    public class AlertService : IAlertService
    {
        private List<Alert> _alerts = new List<Alert>();

        public IReadOnlyList<Alert> Alerts => _alerts.AsReadOnly();

        public AlertService()
        {
        }

        public void AddAlert(string message, AlertType alertType = AlertType.Info)
        {
            this._alerts.Add(new Alert(message, alertType));
        }

        public IReadOnlyList<Alert> ShowAlerts()
        {
            var alerts = Alerts.ToList();
            _alerts.Clear();

            return alerts;
        }
    }

}
