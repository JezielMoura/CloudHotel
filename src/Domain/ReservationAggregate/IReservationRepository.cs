namespace CloudHotel.Domain.ReservationAggregate;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> Get(Guid id);
    Task Add(Reservation reservation);
    Task Update(Reservation reservation);
    Task Delete(Guid id);
}