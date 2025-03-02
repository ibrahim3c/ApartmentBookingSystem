using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Infrastructure.Data;

namespace ApartmentBooking.Infrastructure.Repositories;

internal sealed class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
