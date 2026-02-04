using EXCSLA.Shared.Core.Interfaces;
using EXCSLA.Shared.Core.ValueObjects.Common;

namespace EXCSLA.Shared.Infrastructure.Services;

public class AlertService : IAlertService
{
    private List<Alert> _alerts = new();

    public IReadOnlyList<Alert> Alerts => _alerts.AsReadOnly();

    public AlertService()
    {
    }

    public void AddAlert(string message, AlertType alertType = AlertType.Info)
    {
        _alerts.Add(new Alert(message, alertType));
    }

    public IReadOnlyList<Alert> ShowAlerts()
    {
        var alerts = Alerts.ToList();
        _alerts.Clear();

        return alerts;
    }
}
