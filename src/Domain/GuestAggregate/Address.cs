namespace CloudHotel.Domain.GuestAggregate;

public sealed record Address(
    string Street,
    string PostalCode,
    string City,
    string State,
    string Country
)
{
    public static Address Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
}