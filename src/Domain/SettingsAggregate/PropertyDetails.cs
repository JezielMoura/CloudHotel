
namespace CloudHotel.Domain.SettingsAggregate;

public sealed class PropertyDetails : ValueObject
{
    public string Name { get; }
    public string Email { get; }
    public string Phone { get; } 
    public string Document { get; }
    public byte[] Image { get; }

    public PropertyDetails(string name, string email, string phone, string document, byte[] image)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        Image = image;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Email;
        yield return Phone;
        yield return Document;
    }
}