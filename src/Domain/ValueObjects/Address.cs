using Ardalis.GuardClauses;
using EXCSLA.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EXCSLA.Shared.Domain.ValueObjects;

/// <summary>
/// The Address value object is designed to be a standard US based street address. Because this is a 
/// value object, any changes to this object should result in a new object creation. Thus there is no
/// public setting of properties.
/// </summary>
public class Address : ValueObject
{
    public string Address1 { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Zip { get; private set; }

    /// <summary>
    /// This is an entity framework required constructor, and should not be used by the programmer. Because
    /// this is a value object there is not way to set the values of its properties, making this constructor 
    /// usesless to anyone other than ORM's.
    /// </summary>
    public Address() { } // Required by EF

    /// <summary>
    /// Creates a new Address value object.
    /// </summary>
    /// <param name="address1">The first line of an american type street address. This parameter is required and cannot be empty.</param>
    /// <param name="address2">The second line of an american type street address. This can be an empty string.</param>
    /// <param name="city">The city of an american type street address. This parameter is required and cannot be emtpy.</param>
    /// <param name="state">The state of an american type street address. This requires the abbreviation of the state, must be two characters, and is required.</param>
    /// <param name="zip">The postal code of an american type street address. This parameter is required and cannont be empty.</param>
    public Address(string address1, string address2, string city, string state, string zip)
    {
        Guard.Against.NullOrWhiteSpace(address1, nameof(address1));
        Guard.Against.NullOrWhiteSpace(city, nameof(city));
        Guard.Against.NullOrWhiteSpace(state, nameof(state));
        Guard.Against.MinMaxLengthGuard(state, 2, 2);
        Guard.Against.NullOrWhiteSpace(zip, nameof(zip));

        this.Address1 = address1;
        this.Address2 = address2;
        this.City = city;
        this.State = state;
        this.Zip = zip;
    }
}
