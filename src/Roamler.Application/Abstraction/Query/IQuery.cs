using MediatR;
using Roamler.Domain.Shared;

namespace Roamler.Application.Abstraction.Query;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
        
}