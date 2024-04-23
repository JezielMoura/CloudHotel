using CloudHotel.Domain.ReservationAggregate;
using CloudHotel.Infrastructure.Persistence.Context;

namespace CloudHotel.Infrastructure.Persistence.Repositories;

internal sealed class ReservationRepository(AppDbContext dbContext) : IReservationRepository
{
    public async Task<Reservation?> Get(Guid id) =>
        await dbContext.Reservations.FindAsync(id);
        
    public async Task Add(Reservation reservation) =>
        await dbContext.Reservations.AddAsync(reservation);

    public async Task Update(Reservation reservation) =>
        await Task.FromResult(dbContext.Reservations.Update(reservation));

    public async Task Delete(Guid id)
    {
        if (await dbContext.Reservations.FindAsync(id) is {} reservation)
            dbContext.Reservations.Remove(reservation);
    }
}