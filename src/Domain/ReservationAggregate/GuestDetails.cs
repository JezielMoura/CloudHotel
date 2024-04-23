namespace CloudHotel.Domain.ReservationAggregate;

public sealed record GuestDetails(
    Guid Id, 
    string Name);