using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.GetReservation;

public sealed record GetReservationStatusResponse(int Value, string Description)
{
    public static GetReservationStatusResponse Create(ReservationStatus status) =>
        new (status.Value, status.Description);
}

public sealed record GetReservationResponse(
    Guid Id,
    DateOnly Arrival,
    DateOnly Departure,
    decimal Price,
    Guid RoomId,
    string RoomCode,
    Guid GuestId,
    string GuestName,
    string GuestEmail,
    string GuestPhone,
    string GuestDocumentNumber,
    string GuestDocumentType,
    string AddressPostalCode,
    string AddressStreet,
    string AddressCity,
    string AddressState,
    string AddressCountry,
    int TotalNights,
    decimal NightPrice,
    DateTime? CreatedOn,
    int Status,
    IEnumerable<GetReservationStatusResponse> ReservationStatuses
)
{
    public static GetReservationResponse Create(Reservation reservation, Guest guest) =>
        new(
            reservation.Id, 
            reservation.Arrival, 
            reservation.Departure, 
            reservation.Price, 
            reservation.Room.Id, 
            reservation.Room.Code,
            guest.Id,
            guest.Name,
            guest.Email,
            guest.Phone,
            guest.Document.Number,
            guest.Document.Type,
            guest.Address.PostalCode,
            guest.Address.Street,
            guest.Address.City,
            guest.Address.State,
            guest.Address.Country,
            reservation.GetNightsNumber(),
            reservation.GetAveragePricePerNight(),
            reservation.CreatedOn,
            reservation.Status.Value,
            ReservationStatus.GetAll().Select(GetReservationStatusResponse.Create));
}