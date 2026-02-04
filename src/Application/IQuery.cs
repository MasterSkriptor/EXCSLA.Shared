namespace EXCSLA.Shared.Application;

/// <summary>
/// Marker interface for queries that retrieve data without modifying state.
/// Queries represent an intent to read data from the system.
/// </summary>
/// <typeparam name="TResult">The type of result returned by the query.</typeparam>
public interface IQuery<out TResult>
{
}
