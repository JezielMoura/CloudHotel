using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Abstractions.Persistence;

public interface IAppDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Guest> Guests { get; }
}