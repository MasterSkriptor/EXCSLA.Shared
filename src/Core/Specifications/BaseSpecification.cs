using EXCSLA.Shared.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EXCSLA.Shared.Core.Specifications;

/// <summary>
/// Base class for implementing the Specification pattern for building reusable, composable queries.
/// </summary>
/// <typeparam name="T">The type of entity this specification queries.</typeparam>
/// <remarks>
/// Inherit from this class to create domain-specific query objects that encapsulate filtering,
/// sorting, and pagination logic. This promotes code reuse and keeps queries organized.
/// 
/// Example:
/// <code>
/// public class ActiveProductsSpecification : BaseSpecification&lt;Product&gt;
/// {
///     public ActiveProductsSpecification()
///         : base(p => p.IsActive)
///     {
///         AddInclude(p => p.Category);
///         ApplyOrderBy(p => p.Name);
///     }
/// }
/// </code>
/// </remarks>
public abstract class BaseSpecification<T> : ISpecification<T>
{
    /// <inheritdoc/>
    public Expression<Func<T, bool>> Criteria { get; }

    /// <inheritdoc/>
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    /// <inheritdoc/>
    public List<string> IncludeStrings { get; } = new List<string>();

    /// <inheritdoc/>
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    /// <inheritdoc/>
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    /// <inheritdoc/>
    public Expression<Func<T, object>>? GroupBy { get; private set; }

    /// <inheritdoc/>
    public int Take { get; private set; }

    /// <inheritdoc/>
    public int Skip { get; private set; }

    /// <inheritdoc/>
    public bool isPagingEnabled { get; private set; } = false;

    /// <summary>
    /// Initializes a new instance of the BaseSpecification class with a filter criteria.
    /// </summary>
    /// <param name=\"criteria\">The filter expression to apply to the query.</param>
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    /// <summary>
    /// Adds an expression-based include for eager loading related entities.
    /// </summary>
    /// <param name=\"includeExpression\">The expression for the related entity to load.</param>
    /// <remarks>
    /// Example: <code>AddInclude(p => p.Orders)</code>
    /// </remarks>
    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    /// <summary>
    /// Adds a string-based include for eager loading related entities (complex nested navigation).
    /// </summary>
    /// <param name="includeString">The string path for the related entity to load.</param>
    /// <remarks>
    /// Example: <code>AddInclude("Orders.OrderLines.Product")</code>
    /// Use this for deeply nested navigation properties.
    /// </remarks>
    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    /// <summary>
    /// Applies pagination parameters to the specification.
    /// </summary>
    /// <param name="skip">The number of records to skip.</param>
    /// <param name="take">The number of records to take.</param>
    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        isPagingEnabled = true;
    }

    /// <summary>
    /// Applies ascending sort order to the specification.
    /// </summary>
    /// <param name="orderByExpression">The expression to sort by in ascending order.</param>
    protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    /// <summary>
    /// Applies descending sort order to the specification.
    /// </summary>
    /// <param name="orderByDescendingExpression">The expression to sort by in descending order.</param>
    protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    /// <summary>
    /// Applies a grouping expression for aggregation operations.
    /// </summary>
    /// <param name="groupByExpression">The expression to group by.</param>
    protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
}