using ApartmentBooking.Domain.Apartments;
using MediatR;

namespace ApartmentBooking.Domain.Reviews
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
