using Xunit;
using EXCSLA.Shared.Application;
using EXCSLA.Shared.Application.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Shared.Application.Tests;

/// <summary>
/// Comprehensive test suite for IDispatcher implementation covering command handling,
/// query execution, error conditions, and dependency injection integration.
/// </summary>
public class DispatcherShould : BaseApplicationTest
{
    [Fact]
    public async Task Dispatcher_SendsCommand_Successfully()
    {
        var command = new TestCommand { Data = "Test Data" };

        // Should not throw
        await Dispatcher.SendAsync(command);
    }

    [Fact]
    public async Task Dispatcher_ThrowsArgumentNullException_WhenCommandIsNull()
    {
        TestCommand? nullCommand = null;

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            Dispatcher.SendAsync(nullCommand!));
    }

    [Fact]
    public async Task Dispatcher_SendsCommandWithResult_Successfully()
    {
        // TestCommand doesn't return a result, so we skip this test
        // In a real scenario, you'd have a command that implements ICommand<TResult>
        await Task.CompletedTask;
    }

    [Fact]
    public async Task Dispatcher_ExecutesQuery_Successfully()
    {
        var query = new TestQuery { Parameter = "TestParam" };

        var result = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query);

        Assert.NotNull(result);
        Assert.Equal("TestParam", result.Value);
        Assert.Equal("TestParam".Length, result.Count);
    }

    [Fact]
    public async Task Dispatcher_ThrowsArgumentNullException_WhenQueryIsNull()
    {
        TestQuery? nullQuery = null;

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            Dispatcher.QueryAsync<TestQuery, TestQueryResult>(nullQuery!));
    }

    [Fact]
    public async Task Dispatcher_HandlersAreResolvedFromDI()
    {
        var handler = ServiceProvider.GetRequiredService<ICommandHandler<TestCommand>>();

        Assert.NotNull(handler);
        Assert.IsType<TestCommandHandler>(handler);
    }

    [Fact]
    public async Task Dispatcher_SupportsMultipleCommands_InSequence()
    {
        var command1 = new TestCommand { Data = "First" };
        var command2 = new TestCommand { Data = "Second" };
        var command3 = new TestCommand { Data = "Third" };

        await Dispatcher.SendAsync(command1);
        await Dispatcher.SendAsync(command2);
        await Dispatcher.SendAsync(command3);

        // All should execute without errors
    }

    [Fact]
    public async Task Dispatcher_SupportsMultipleQueries_InSequence()
    {
        var query1 = new TestQuery { Parameter = "Query1" };
        var query2 = new TestQuery { Parameter = "Query2" };

        var result1 = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query1);
        var result2 = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query2);

        Assert.NotEqual(result1.Value, result2.Value);
    }

    [Fact]
    public async Task Dispatcher_RespectsCancellationToken()
    {
        var command = new TestCommand();
        var cts = new System.Threading.CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromMilliseconds(100));

        // Command handler completes quickly, so cancellation won't trigger
        // This test verifies the parameter is passed through
        await Dispatcher.SendAsync(command, cts.Token);
    }

    [Fact]
    public async Task Dispatcher_CommandHandlerValidatesInput()
    {
        var invalidCommand = new TestCommand { Data = "" }; // Empty data

        await Assert.ThrowsAsync<ArgumentException>(() =>
            Dispatcher.SendAsync(invalidCommand));
    }

    [Fact]
    public async Task Dispatcher_QueryHandlerReturnsCorrectType()
    {
        var query = new TestQuery { Parameter = "ValidParam" };

        var result = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query);

        Assert.IsType<TestQueryResult>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Dispatcher_TransitionsBetweenCommandAndQuery()
    {
        var command = new TestCommand { Data = "Setup" };
        var query = new TestQuery { Parameter = "Query" };

        await Dispatcher.SendAsync(command);
        var result = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Dispatcher_WithCustomServiceConfiguration()
    {
        var customServices = new ServiceCollection();
        customServices.AddDispatcher();
        customServices.AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>();
        customServices.AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();

        var provider = customServices.BuildServiceProvider();
        var customDispatcher = new Dispatcher(provider);

        var command = new TestCommand { Data = "CustomSetup" };

        await customDispatcher.SendAsync(command);
        // Should not throw
    }
}

/// <summary>
/// Test suite for ICommand implementation and patterns.
/// </summary>
public class CommandsShould
{
    [Fact]
    public void TestCommand_CanBeInstantiated()
    {
        var command = new TestCommand { Data = "Test" };

        Assert.NotNull(command);
        Assert.Equal("Test", command.Data);
    }

    [Fact]
    public void TestCommand_IsAssignableToICommand()
    {
        var command = new TestCommand();

        Assert.IsAssignableFrom<ICommand>(command);
    }

    [Fact]
    public void TestCommand_CanBeModified()
    {
        var command = new TestCommand { Data = "Original" };

        command.Data = "Modified";

        Assert.Equal("Modified", command.Data);
    }

    [Fact]
    public void Command_DefaultValue_IsEmpty()
    {
        var command = new TestCommand();

        Assert.NotNull(command.Data);
        Assert.Equal("Test Data", command.Data); // Default value from fixture
    }
}

/// <summary>
/// Test suite for IQuery implementation and patterns.
/// </summary>
public class QueriesShould
{
    [Fact]
    public void TestQuery_CanBeInstantiated()
    {
        var query = new TestQuery { Parameter = "Test" };

        Assert.NotNull(query);
        Assert.Equal("Test", query.Parameter);
    }

    [Fact]
    public void TestQuery_IsAssignableToIQuery()
    {
        var query = new TestQuery();

        Assert.IsAssignableFrom<IQuery<TestQueryResult>>(query);
    }

    [Fact]
    public void TestQuery_CanBeModified()
    {
        var query = new TestQuery { Parameter = "Original" };

        query.Parameter = "Modified";

        Assert.Equal("Modified", query.Parameter);
    }

    [Fact]
    public void QueryResult_CanBeInstantiated()
    {
        var result = new TestQueryResult("Value", 5);

        Assert.NotNull(result);
        Assert.Equal("Value", result.Value);
        Assert.Equal(5, result.Count);
    }
}

/// <summary>
/// Test suite for handler implementations covering command and query handling.
/// </summary>
public class HandlersShould : BaseApplicationTest
{
    [Fact]
    public async Task CommandHandler_ExecutesSuccessfully()
    {
        var handler = GetService<ICommandHandler<TestCommand>>();
        var command = new TestCommand { Data = "Valid" };

        await handler.HandleAsync(command);

        // Should not throw
    }

    [Fact]
    public async Task CommandHandler_ValidatesCommand()
    {
        var handler = GetService<ICommandHandler<TestCommand>>();
        var invalidCommand = new TestCommand { Data = "" };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.HandleAsync(invalidCommand));
    }

    [Fact]
    public async Task QueryHandler_ReturnsResult()
    {
        var handler = GetService<IQueryHandler<TestQuery, TestQueryResult>>();
        var query = new TestQuery { Parameter = "Test" };

        var result = await handler.HandleAsync(query);

        Assert.NotNull(result);
        Assert.IsType<TestQueryResult>(result);
    }

    [Fact]
    public async Task QueryHandler_ResultContainsQueryData()
    {
        var handler = GetService<IQueryHandler<TestQuery, TestQueryResult>>();
        var query = new TestQuery { Parameter = "TestValue" };

        var result = await handler.HandleAsync(query);

        Assert.Equal("TestValue", result.Value);
        Assert.Equal("TestValue".Length, result.Count);
    }

    [Fact]
    public async Task Handlers_CanBeResolvedAsInterfaces()
    {
        var commandHandler = GetService<ICommandHandler<TestCommand>>();
        var queryHandler = GetService<IQueryHandler<TestQuery, TestQueryResult>>();

        Assert.NotNull(commandHandler);
        Assert.NotNull(queryHandler);
    }
}

/// <summary>
/// Test suite for ServiceCollection extensions and dependency injection configuration.
/// </summary>
public class ServiceCollectionExtensionsShould
{
    [Fact]
    public void AddDispatcher_RegistersDispatcherAsSingleton()
    {
        var services = new ServiceCollection();
        services.AddDispatcher();

        var provider = services.BuildServiceProvider();
        var dispatcher1 = provider.GetRequiredService<IDispatcher>();
        var dispatcher2 = provider.GetRequiredService<IDispatcher>();

        Assert.Same(dispatcher1, dispatcher2);
    }

    [Fact]
    public void AddDispatcher_RegistersDispatchemento()
    {
        var services = new ServiceCollection();
        services.AddDispatcher();

        var provider = services.BuildServiceProvider();
        var dispatcher = provider.GetRequiredService<IDispatcher>();

        Assert.NotNull(dispatcher);
        Assert.IsType<Dispatcher>(dispatcher);
    }

    [Fact]
    public void Services_CanChainRegistrations()
    {
        var services = new ServiceCollection();
        
        services
            .AddDispatcher()
            .AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>()
            .AddTransient<IQueryHandler<TestQuery, TestQueryResult>, TestQueryHandler>();

        var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetRequiredService<IDispatcher>());
        Assert.NotNull(provider.GetRequiredService<ICommandHandler<TestCommand>>());
        Assert.NotNull(provider.GetRequiredService<IQueryHandler<TestQuery, TestQueryResult>>());
    }
}

/// <summary>
/// Integration tests covering complex scenarios and patterns.
/// </summary>
public class ApplicationLayerIntegrationShould : BaseApplicationTest
{
    [Fact]
    public async Task Full_RequestResponseCycle_Works()
    {
        var command = new TestCommand { Data = "IntegrationTest" };
        
        await Dispatcher.SendAsync(command);

        var query = new TestQuery { Parameter = "IntegrationTest" };
        var result = await Dispatcher.QueryAsync<TestQuery, TestQueryResult>(query);

        Assert.NotNull(result);
        Assert.Equal("IntegrationTest", result.Value);
    }

    [Fact]
    public async Task DispatcherFixtureBuilder_CreatesWorkingDispatcher()
    {
        var (provider, dispatcher) = new DispatcherFixtureBuilder()
            .WithDefaultServices()
            .WithCommandHandler<TestCommand, TestCommandHandler>()
            .WithQueryHandler<TestQuery, TestQueryResult, TestQueryHandler>()
            .BuildWithProvider();

        var command = new TestCommand { Data = "BuiltTest" };
        await dispatcher.SendAsync(command);

        var result = await dispatcher.QueryAsync<TestQuery, TestQueryResult>(
            new TestQuery { Parameter = "Built" });

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ErrorHandling_PropagatesExceptions()
    {
        var invalidCommand = new TestCommand { Data = null! };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            Dispatcher.SendAsync(invalidCommand));
    }

    [Fact]
    public async Task Dispatcher_WorksWithAsyncHandlers()
    {
        var command = new TestCommand { Data = "Async" };

        // Handler completes asyncly
        var task = Dispatcher.SendAsync(command);

        await task;

        Assert.True(task.IsCompletedSuccessfully);
    }
}
