using EXCSLA.Shared;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using EXCSLA.Shared.Core.Exceptions;

namespace EXCSLA.Shared.Core.ValueObjects;

/// <summary>
/// A standard email address value object. Because this is a 
/// value object, any changes to this object should result in a new object creation. Thus there is no
/// public setting of properties.
/// </summary>
public class Email : ValueObject
{
    public string Address {get; private set;}
    public string Domain {get; private set;}

    /// <summary>
    /// This is an entity framework required constructor, and should not be used by the programmer. Because
    /// this is a value object there is not way to set the values of its properties, making this constructor 
    /// usesless to anyone other than ORM's.
    /// </summary>
    public Email() { } // Required by EF

    /// <summary>
    /// Creates a standard email address value object.
    /// </summary>
    /// <param name="emailAddress">A string containing the full email address.</param>
    public Email(string emailAddress)
    {
        if(!IsValidEmail(emailAddress)) throw new EmailAddressOutOfBoundsException("The email address is malformed.");
        var list = emailAddress.Split('@');
        SetAddress(list[0]);
        SetDomain(list[1]);

    }

    /// <summary>
    /// Creates a standard email address value object
    /// </summary>
    /// <param name="address">The user name portion of an email address.</param>
    /// <param name="domain">The domain name portion of an email address.</param>
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

    private void SetDomain(string domain)
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