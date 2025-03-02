using ApartmentBooking.Domain.Users;
using ApartmentBooking.Infrastructure.Data;

namespace ApartmentBooking.Infrastructure.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

}

