namespace CloudHotel.Domain.GuestAggregate;

public interface IGuestRepository : IRepository<Guest>
{
    Task Add(Guest guest);
}