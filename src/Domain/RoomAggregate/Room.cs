namespace CloudHotel.Domain.RoomAggregate;

public sealed class Room : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Code { get; private set; }
    public IList<string> Photos { get; private set; }

    public Room(Guid id, string name, string description, string code, List<string>? photos = null)
    {
        DomainException.ThrowIfNullOrEmpty(name, "Name cannot be null or empry", nameof(name), "Room.EmptyName");
        DomainException.ThrowIfNullOrEmpty(description, "Name cannot be null or empry", nameof(description), "Room.EmptyDescription");
        DomainException.ThrowIfNullOrEmpty(code, "Code cannot be null or empry", nameof(code), "Room.EmptyCode");

        Id = id;
        Name = name;
        Description = description;
        Code = code;
        Photos = photos ?? [];
    }

    #nullable disable
    private Room()
    { }
}