using EXCSLA.Shared;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using EXCSLA.Shared.Core.Exceptions;

namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    public class Email : ValueObject
    {
        public string Address {get; private set;}
        public string Domain {get; private set;}

        public Email(string emailAddress)
        {
            if(!IsValidEmail(emailAddress)) throw new EmailAddressOutOfBoundsException("The email address is malformed.");
            var list = emailAddress.Split('@');
            SetAddress(list[0]);
            SetDomain(list[1]);

        }

        public Email(string address, string domain)
        {
            this.Address = address;
            this.Domain = domain;
        }

        private void SetAddress(string address)
        {
            if(address.Length < 0 || address.Length > 50) throw new EmailAddressOutOfBoundsException("The email address' user name must be shorter than 50 characters.");

            this.Address = address;
        }

        public void SetDomain(string domain)
        {
            this.Domain = domain;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Address + "@" + this.Domain;
        }
    }
}