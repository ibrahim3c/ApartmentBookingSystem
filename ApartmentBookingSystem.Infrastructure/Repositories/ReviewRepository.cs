using ApartmentBooking.Domain.Reviews;
using ApartmentBooking.Infrastructure.Data;

namespace ApartmentBooking.Infrastructure.Repositories
{
    internal sealed  class ReviewRepository : BaseRepository<Review>,IReviewRepository
    {
        public ReviewRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
