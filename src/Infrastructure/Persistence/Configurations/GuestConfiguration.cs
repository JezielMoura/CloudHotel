using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Infrastructure.Persistence.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("Guests");
        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasColumnType($"varchar(120)");

        builder
            .Property(p => p.Email)
            .IsRequired()
            .HasColumnType($"varchar(120)");

        builder
            .Property(p => p.Phone)
            .IsRequired()
            .HasColumnType($"varchar(30)");

        builder
            .ComplexProperty(p => p.Document, b => {
                b.Property(p => p.Number).HasColumnType("varchar(30)").HasColumnName("DocumentNumber");
                b.Property(p => p.Type).HasColumnType("varchar(30)").HasColumnName("DocumentType");
            });

        builder
            .ComplexProperty(p => p.Address, b => {
                b.Property(p => p.Street).HasColumnType("varchar(120)").HasColumnName("AddressStreet");
                b.Property(p => p.PostalCode).HasColumnType("varchar(30)").HasColumnName("AddresPostalCode");
                b.Property(p => p.City).HasColumnType("varchar(60)").HasColumnName("AddressCity");
                b.Property(p => p.State).HasColumnType("varchar(60)").HasColumnName("AddressState");
                b.Property(p => p.Country).HasColumnType("varchar(60)").HasColumnName("AddressCountry");
            });
    }
}