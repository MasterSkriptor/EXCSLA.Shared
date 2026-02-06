using System;

namespace EXCSLA.Shared.Core;

/// <summary>
/// Attribute to exclude a property or field from ValueObject equality comparisons.
/// </summary>
/// <remarks>
/// By default, ValueObject equality is based on all public properties and fields.
/// Use this attribute on properties or fields that should not be considered in equality checks.
/// 
/// Example:
/// <code>
/// public class Product : ValueObject
/// {
///     public string Name { get; private set; }
///     
///     [IgnoreMember]
///     public DateTime CreatedDate { get; private set; }
/// }
/// </code>
/// 
/// In this example, two products with identical names but different creation dates
/// would be considered equal.
/// 
/// Source: https://github.com/jhewlett/ValueObject
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreMemberAttribute : Attribute
{
}