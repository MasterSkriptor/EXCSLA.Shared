namespace EXCSLA.Shared.Application;

/// <summary>
/// Marker interface for commands that modify state.
/// Commands represent an intent to change the system state.
/// </summary>
public interface ICommand
{
}

/// <summary>
/// Marker interface for commands that modify state and return a result.
/// </summary>
/// <typeparam name="TResult">The type of result returned by the command.</typeparam>
public interface ICommand<out TResult>
{
}
