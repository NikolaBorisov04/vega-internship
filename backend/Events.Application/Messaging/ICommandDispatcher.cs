namespace Events.Application.Messaging;

public interface ICommandDispatcher
{
    Task<Result> Send<TCommand>(TCommand command, CancellationToken ct) where TCommand : ICommand;

    Task<Result<TResponse>> Send<TCommand, TResponse>(TCommand command, CancellationToken ct) where TCommand : ICommand<TResponse>;
}