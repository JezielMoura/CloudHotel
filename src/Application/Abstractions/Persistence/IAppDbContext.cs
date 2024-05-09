using CloudHotel.Domain.GuestAggregate;
using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Abstractions.Persistence;

public interface IAppDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Guest> Guests { get; }
    DbSet<SettingsModel> Settings { get; }
}