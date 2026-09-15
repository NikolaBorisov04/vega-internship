using Microsoft.Extensions.DependencyInjection;

namespace Events.Application.Messaging;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<Result> Send<TCommand>(TCommand command, CancellationToken ct) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return handler.Handle(command, ct);
    }

    public Task<Result<TResponse>> Send<TCommand, TResponse>(TCommand command, CancellationToken ct) where TCommand : ICommand<TResponse>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();

        return handler.Handle(command, ct);
    }
}