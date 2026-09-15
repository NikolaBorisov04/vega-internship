namespace Events.Application.Messaging;

public interface IQueryDispatcher
{
    Task<Result<TResponse>> Send<TQuery, TResponse>(TQuery query, CancellationToken ct) where TQuery : IQuery<TResponse>;
}