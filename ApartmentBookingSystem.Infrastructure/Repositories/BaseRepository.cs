using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApartmentBooking.Infrastructure.Repositories;

internal class BaseRepository<T> where T : Entity
{
    // make it protected to use it in custom operation in others repos
    protected readonly AppDbContext appDbContext;

    public BaseRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    //The ? after T means that T is nullable, meaning it can return null if no record is found.
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        appDbContext.Add(entity);
    }

}
