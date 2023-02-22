using MediatR;
using Roamler.Domain.Shared;

namespace Roamler.Application.Abstraction.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
        
}