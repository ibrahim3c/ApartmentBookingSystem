using ApartmentBooking.Domain.Abstraction;
using MediatR;

namespace ApartmentBooking.Application.Abstractions.Messaging;

//without Response
public interface ICommand:IRequest<Result>,IBaseCommand
{
}
// with Response
public interface ICommand<TResponse> : IRequest<Result<TResponse>>,IBaseCommand
{
}

// the value of it is that we can apply generic constraints in our pipeline behaviors : we will use it later
//Helps enforce constraints in pipeline behaviors (e.g., logging, validation, transaction handling).
public interface IBaseCommand
{
}