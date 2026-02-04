namespace EXCSLA.Shared.Application;

/// <summary>
/// Dispatcher interface for sending commands and queries to their handlers.
/// This is a simple implementation that doesn't require external dependencies like MediatR.
/// </summary>
public interface IDispatcher
{
    /// <summary>
    /// Sends a command to its handler asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to send.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;

    /// <summary>
    /// Sends a command to its handler asynchronously and returns a result.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to send.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the command.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a result.</returns>
    Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>;

    /// <summary>
    /// Sends a query to its handler asynchronously and returns a result.
    /// </summary>
    /// <typeparam name="TQuery">The type of query to send.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
    /// <param name="query">The query to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a result.</returns>
    Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;
}
