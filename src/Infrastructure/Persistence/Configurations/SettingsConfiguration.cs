using CloudHotel.Domain.SettingsAggregate;

namespace CloudHotel.Infrastructure.Persistence.Configurations;

internal sealed class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.ToTable("Settings");
        builder.HasKey(p => p.Id);

        builder
            .OwnsOne(p => p.Property, b => {
                b.Property(p => p.Name).HasColumnType("varchar(60)").HasColumnName("PropertyName");
                b.Property(p => p.Email).HasColumnType("varchar(60)").HasColumnName("PropertyEmail");
                b.Property(p => p.Phone).HasColumnType("varchar(20)").HasColumnName("PropertyPhone");
                b.Property(p => p.Document).HasColumnType("varchar(30)").HasColumnName("PropertyDocument");
                b.Property(p => p.Image).HasColumnType("bytea").HasColumnName("PropertyImage");
            });
    }
}