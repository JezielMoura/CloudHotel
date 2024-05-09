namespace CloudHotel.Domain.SettingsAggregate;

public sealed partial class Settings : Entity, IAggregateRoot
{
    public static readonly Settings Default = new ( 
        Guid.NewGuid(),
        new(
            name: "CloudHotel",
            email: "support@cloudhotel.com",
            phone: "+55 (11) 99999-0000",
            document: "10.543.234/0001-79",
            image: []
        )
    );

    public PropertyDetails Property { get; private set; }

    public Settings(Guid id, PropertyDetails property)
    {
        Id = id;
        Property = property;
    }

    #nullable disable
    private Settings() {}
}