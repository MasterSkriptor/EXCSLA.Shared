using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Core.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendEmailAsync(string email, string subject, string message, string from);
}
