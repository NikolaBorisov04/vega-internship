using Microsoft.Extensions.DependencyInjection;

namespace Events.Application.Messaging;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<Result<TResponse>> Send<TQuery, TResponse>(TQuery query, CancellationToken ct) where TQuery : IQuery<TResponse>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();

        return handler.Handle(query, ct);
    }
}