using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Infrastructure.Persistence.Context;

namespace CloudHotel.Infrastructure.Persistence.Repositories;

internal sealed class GuestRepository(AppDbContext dbContext) : IGuestRepository
{
    public async Task Add(Guest guest) =>
        await dbContext.Guests.AddAsync(guest);

    public async ValueTask<Guest?> Get(Guid id) =>
        await dbContext.Guests.FindAsync(id);
}