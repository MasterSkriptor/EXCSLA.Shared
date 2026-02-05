using EXCSLA.Shared.Application;

namespace EXCSLA.Shared.Application.Tests.Fixtures;

/// <summary>
/// Test command for verifying ICommand and ICommandHandler implementations.
/// </summary>
public sealed class TestCommand : ICommand
{
    public string Data { get; set; } = "Test Data";
}

/// <summary>
/// Handler for TestCommand that performs basic validation and processing.
/// </summary>
public sealed class TestCommandHandler : ICommandHandler<TestCommand>
{
    public Task HandleAsync(TestCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        if (string.IsNullOrWhiteSpace(command.Data))
        {
            throw new ArgumentException("Command data cannot be empty.", nameof(command));
        }

        return Task.CompletedTask;
    }
}

/// <summary>
/// Test query for verifying IQuery and IQueryHandler implementations.
/// </summary>
public sealed class TestQuery : IQuery<TestQueryResult>
{
    public string Parameter { get; set; } = "Test Parameter";
}

/// <summary>
/// Result type for TestQuery.
/// </summary>
public sealed class TestQueryResult
{
    public string Value { get; set; }
    public int Count { get; set; }

    public TestQueryResult(string value, int count)
    {
        Value = value;
        Count = count;
    }
}

/// <summary>
/// Handler for TestQuery that returns a simple result.
/// </summary>
public sealed class TestQueryHandler : IQueryHandler<TestQuery, TestQueryResult>
{
    public Task<TestQueryResult> HandleAsync(TestQuery query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        
        var result = new TestQueryResult(query.Parameter, query.Parameter.Length);
        return Task.FromResult(result);
    }
}
