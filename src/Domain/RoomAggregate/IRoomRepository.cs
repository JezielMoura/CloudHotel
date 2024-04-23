namespace CloudHotel.Domain.RoomAggregate;

public interface IRoomRepository : IRepository<Room>
{
    Task Add(IEnumerable<Room> rooms);
    Task Update(Room room);
    Task Delete(Guid id);
}