using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.ReservationAggregate;
using CloudHotel.Domain.RoomAggregate;

namespace CloudHotel.Infrastructure.Persistence.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Arrival);
        builder.HasIndex(x => x.Departure);

        builder
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        builder
            .Property(p => p.Status)
            .HasConversion(v => v.Value, v => ReservationStatus.FromNumber(v));

        builder
            .OwnsOne(p => p.Room, b => {
                b.HasOne<Room>().WithMany().HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Property(p => p.Id).HasColumnName("RoomId");
                b.Property(p => p.Code).HasColumnType("varchar(60)").HasColumnName("RoomCode");
            });

        builder
            .OwnsOne(p => p.Guest, b => {
                b.HasOne<Guest>().WithMany().HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Property(p => p.Id).HasColumnName("GuestId");
                b.Property(p => p.Name).HasColumnType("varchar(120)").HasColumnName("GuestName");
            });
    }
}
