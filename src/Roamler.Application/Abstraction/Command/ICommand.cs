using MediatR;
using Roamler.Domain.Shared;

namespace Roamler.Application.Abstraction.Command;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
        
}