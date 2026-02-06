using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXCSLA.Shared.Core.Interfaces;

/// <summary>
/// Specification interface for building complex queries with filtering, sorting, eager loading, and pagination.
/// Implements the Specification pattern to encapsulate query logic in a reusable, composable manner.
/// </summary>
/// <typeparam name="T">The type of entity this specification queries.</typeparam>
/// <remarks>
/// The Specification pattern allows you to build complex queries by composing filtering, ordering,
/// and pagination concerns. This promotes code reuse and keeps queries organized by domain concept.
/// Use BaseSpecification&lt;T&gt; as the base class for creating concrete specifications.
/// </remarks>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the filter criteria for the query.
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Gets the list of related entities to eagerly load via expression.
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets the list of related entity navigation properties to eagerly load as strings.
    /// Useful for complex nested navigation that cannot be expressed as expressions.
    /// </summary>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// Gets the ascending sort order expression.
    /// </summary>
    Expression<Func<T, object>> OrderBy { get; }

    /// <summary>
    /// Gets the descending sort order expression.
    /// </summary>
    Expression<Func<T, object>> OrderByDescending { get; }

    /// <summary>
    /// Gets the grouping expression for aggregation operations.
    /// </summary>
    Expression<Func<T, object>> GroupBy { get; }

    /// <summary>
    /// Gets the number of records to take (for pagination).
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Gets the number of records to skip before taking (for pagination offset).
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets whether pagination is enabled for this specification.
    /// </summary>
    bool isPagingEnabled { get;}
}