using ApartmentBooking.Domain.Abstraction;
using MediatR;

namespace ApartmentBooking.Application.Abstractions.Messaging;

// without response
public interface ICommandHanlder<TCommand>:IRequestHandler<TCommand,Result>
    where TCommand : ICommand
{
}

// with response
public interface ICommandHanlder<TCommand,TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}