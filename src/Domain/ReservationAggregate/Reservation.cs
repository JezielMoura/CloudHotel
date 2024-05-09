namespace CloudHotel.Domain.ReservationAggregate;

public sealed class Reservation : Entity, IAggregateRoot
{
    public DateOnly Arrival { get; private set; }
    public DateOnly Departure { get; private set; }
    public decimal Price { get; private set; }
    public ReservationStatus Status { get; private set; }
    public RoomDetails Room { get; private set; }
    public GuestDetails Guest { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; }

    public Reservation(Guid id, DateOnly arrival, DateOnly departure, decimal price, RoomDetails room)
    {
        Id = id;
        Arrival = arrival;
        Departure = departure;
        Price = price;
        Room = room;
        Status = ReservationStatus.Pendent;
    }

    public void SetGuestDetails(Guid id, string name) =>
        Guest = new(id, name);

    public void SetStatus(ReservationStatus status) =>
        Status = status;

    public int GetNightsNumber() =>
        Departure.DayNumber - Arrival.DayNumber;

    public decimal GetAveragePricePerNight() =>
        Price / GetNightsNumber();

    public void SetCreatedOnWithCurrentDate() =>
        CreatedOn = DateTime.UtcNow;

    #nullable disable
    private Reservation() {}
}
