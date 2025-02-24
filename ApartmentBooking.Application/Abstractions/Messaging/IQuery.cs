using ApartmentBooking.Domain.Abstraction;
using MediatR;

namespace ApartmentBooking.Application.Abstractions.Messaging
{
    /*
     * It is used with MediatR for handling queries.
     * The response is wrapped in a Result<TResponse> 
                            to handle success or failure.
     */
    public interface IQuery<TResponse>:IRequest<Result<TResponse>>
    {
    }
}
