namespace CloudHotel.Domain.ReservationAggregate;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> Get(Guid id);
    Task Add(Reservation reservation);
    Task Update(Reservation reservation);
    Task<int> Count(Guid reservationId, Guid roomId, DateOnly from, DateOnly to);
    Task Delete(Guid id);
}