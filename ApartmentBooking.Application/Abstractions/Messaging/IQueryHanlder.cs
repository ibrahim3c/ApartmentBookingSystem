using ApartmentBooking.Domain.Abstraction;
using MediatR;

namespace ApartmentBooking.Application.Abstractions.Messaging
{
    /*
     * IQueryHandler<TQuery, TResponse>
        A generic interface for query handlers.
        It processes queries and returns results.
     * IRequestHandler<TQuery, Result<TResponse>>
        It extends MediatR’s IRequestHandler<TQuery, Result<TResponse>>, meaning:
            It takes an input query (TQuery).
            It returns a Result<TResponse> (which might contain data or an error).
     * where TQuery : IQuery<TResponse>
        This ensures that TQuery must implement IQuery<TResponse>, 
                          making sure only valid queries are processed.
     */
    public interface IQueryHanlder<TQuery,TResponse>:IRequestHandler<TQuery,Result<TResponse>>
        where TQuery :IQuery<TResponse>
    {
    }
}
