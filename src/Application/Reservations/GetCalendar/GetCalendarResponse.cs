namespace CloudHotel.Application.Reservations.GetCalendar;

public record GetCalendarResponse(string Name, IEnumerable<AvailabilityResponse> Availabilitys, IEnumerable<RoomResponse> Rooms)
{
    public static GetCalendarResponse Create(IGrouping<string, Room> roomGroup, IEnumerable<Reservation> reservations, DateRange range)
    {
        var groupReservations = reservations.Where(x => roomGroup.Select(x => x.Id).Contains(x.Room.Id));
        var availabilitys = range.Dates.Select(date => AvailabilityResponse.Create(date, groupReservations, roomGroup.Count()));
        var roomGroupCalendar = roomGroup.Select(room => RoomResponse.Create(room, groupReservations, range)).OrderBy(x => x.Code);
        return new(roomGroup.Key, availabilitys, roomGroupCalendar);
    }
}

public record AvailabilityResponse(DateOnly Date, int Value)
{
    public static AvailabilityResponse Create(DateOnly date, IEnumerable<Reservation> groupReservationList, int roomCount)
    {
        var availability = roomCount - groupReservationList.Where(x => x.Arrival <= date && x.Departure > date).Count();
        return new(date, availability);
    }
}

public record RoomResponse(string Code, IEnumerable<DayResponse> Days)
{
    public static RoomResponse Create(Room room, IEnumerable<Reservation> groupReservationList, DateRange range)
    {
        var roomReservations = groupReservationList.Where(x => x.Room.Id == room.Id);
        var dayResponseList = range.Dates.Select(date => DayResponse.Create(date, roomReservations));
        return new(room.Code, dayResponseList);
    }
}

public record ReservationResponse(Guid Id, string Guest, int Days, int Status)
{
    public static ReservationResponse Create(Reservation reservation) =>
        new (reservation.Id, reservation.Guest.Name, reservation.GetNightsNumber(), reservation.Status.Value);
};

public record DayResponse(DateOnly Date, ReservationResponse? Reservation)
{
    public static DayResponse Create(DateOnly date, IEnumerable<Reservation> roomReservation)
    {
        var reservation = roomReservation.FirstOrDefault(x => x.Arrival == date);
        var reservationResponse = reservation is not null ? ReservationResponse.Create(reservation) : null;
        return new DayResponse(date, reservationResponse);
    }
}
