using CloudHotel.Domain.RoomAggregate;
using CloudHotel.Infrastructure.Persistence.Context;

namespace CloudHotel.Infrastructure.Persistence.Repositories;

internal sealed class RoomRepository(AppDbContext dbContext) : IRoomRepository
{
    public async Task Add(IEnumerable<Room> rooms) =>
        await dbContext.Rooms.AddRangeAsync(rooms);

    public async Task Update(Room room) =>
        await Task.FromResult(dbContext.Rooms.Update(room));

    public async Task Delete(Guid id)
    {
        if (await dbContext.Rooms.FindAsync(id) is {} room)
            dbContext.Rooms.Remove(room);
    }
}