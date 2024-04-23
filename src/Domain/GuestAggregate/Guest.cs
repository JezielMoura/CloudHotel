namespace CloudHotel.Domain.GuestAggregate;

public sealed class Guest : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public Document Document { get; private set; }
    public Address Address { get; private set; }

    public Guest(Guid id, string name, string email, string phone, Document document, Address address)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        Address = address;
    }

    #nullable disable
    private Guest() {}
}
