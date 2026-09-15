namespace Events.Application.Messaging;

public interface ICommand : IBaseCommand
{
    
}

public interface ICommand<TResponse> : IBaseCommand
{
    
}