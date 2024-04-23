using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Domain.RoomAggregate;

namespace CloudHotel.Infrastructure.Persistence.Configurations;

internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasColumnType($"varchar({CreateRoomValidator.NameMaximumLenght})");

        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasColumnType($"varchar({CreateRoomValidator.DescriptionMaximumLenght})");
    }
}