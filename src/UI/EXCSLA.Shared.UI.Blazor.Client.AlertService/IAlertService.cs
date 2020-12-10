﻿using EXCSLA.Shared.Core.ValueObjects.Common;
using System;
using System.Collections.Generic;

namespace EXCSLA.Shared.UI.Blazor.Client.AlertService
{
    public interface IAlertService
    {
        List<Alert> Alerts { get; set; }

        event EventHandler<Alert> OnAlert;

        void AddAlert(string message);
        void AddAlert(string message, AlertType type);
    }
}