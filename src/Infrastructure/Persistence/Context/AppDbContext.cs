using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.ReservationAggregate;
using CloudHotel.Domain.RoomAggregate;
using CloudHotel.Domain.SettingsAggregate;

namespace CloudHotel.Infrastructure.Persistence.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork, IAppDbContext
{
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Settings> Settings => Set<Settings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<Result<bool, Error>> Commit() =>
        await SaveChangesAsync() > 0 ? true : Error.DefaultDatabaseError;

    public async Task<Result<Guid, Error>> Commit(Guid id) =>
        await SaveChangesAsync() > 0 ? id : Error.DefaultDatabaseError;
}
