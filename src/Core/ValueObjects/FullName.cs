using EXCSLA.Shared;
using Ardalis.GuardClauses;
using System;

namespace EXCSLA.Shared.Core.ValueObjects;

/// <summary>
/// FullName is a standard human name value object
/// </summary>
public class FullName : ValueObject
{
    public string FirstName {get; private set;}
    public string LastName {get; private set;}
    public string MiddleName { get; private set; }

    /// <summary>
    /// This is an entity framework required constructor, and should not be used by the programmer. Because
    /// this is a value object there is not way to set the values of its properties, making this constructor 
    /// usesless to anyone other than ORM's.
    /// </summary>
    public FullName() { } // required by EF

    /// <summary>
    /// Creates a standard human name value object
    /// </summary>
    /// <param name="firstName">The first name of a person.</param>
    /// <param name="lastName">The last name of a person.</param>
    public FullName(string firstName, string lastName)
    {
        this.SetFirstName(firstName);
        this.SetLastName(lastName);
    }

    /// <summary>
    /// Creates a standard human name value object
    /// </summary>
    /// <param name="firstName">The first name of a person.</param>
    /// <param name="middleName">The last name of a person.</param>
    /// <param name="lastName">The middle name of a person.</param>
    public FullName(string firstName, string middleName, string lastName)
    {
        this.SetFirstName(firstName);
        this.SetLastName(lastName);
        this.SetMiddleName(middleName);
    }

    /// <summary>
    /// Returns a string with the name properly formatted
    /// </summary>
    /// <param name="lastNameFirst">Optional: Returns the name formatted; eg. Doe, John</param>
    /// <param name="withMiddleName">Optional: Returns the name formatted; eg. John Adam Doe</param>
    /// <param name="withMiddleInitial">Optional Returns the name formatted; eg. John A. Doe</param>
    /// <returns>Returns the name formatted; eg John Doe</returns>
    public string AsFormatedName(bool lastNameFirst = false, bool withMiddleName = false, bool withMiddleInitial = false)
    {
        if(lastNameFirst) return this.LastName + ", " + this.FirstName;
        if (withMiddleName) return this.FirstName + " " + this.MiddleName + " " + this.LastName;
        if (withMiddleInitial) return this.FirstName + " " + this.MiddleName.Substring(0, 1).ToUpper() + ". " + this.LastName;
        return this.FirstName + " " + this.LastName;
    }

    /// <summary>
    /// Returns a string with the name formatted using AsFormatedName(false)
    /// </summary>
    /// <returns>eg. John Doe</returns>
    public override string ToString()
    {
        return this.AsFormatedName(false);
    }

    private void SetFirstName(string firstName)
    {
        Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));

        this.FirstName = firstName;
    }

    private void SetLastName(string lastName)
    {
        Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));

        this.LastName = lastName;
    }

    private void SetMiddleName(string middleName)
    {
        this.MiddleName = middleName;
    }
}