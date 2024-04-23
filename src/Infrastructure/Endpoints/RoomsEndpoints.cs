using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Application.Rooms.DeleteRoom;
using CloudHotel.Application.Rooms.SearchRoom;
using CloudHotel.Application.Rooms.UpdateRoom;

namespace CloudHotel.Infrastructure.Endpoints;

public static class RoomsEndpoints
{
    public static void MapRoomsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGet("/", GetHandler).WithName("Search (room)");
        group.MapPost("/", PostHandler).Validate<CreateRoomCommand>().WithName("Add (room)");
        group.MapPut("/", PutHandler).Validate<UpdateRoomCommand>().WithName("Update (room)");
        group.MapDelete("/{id}", DeleteHandler).WithName("Delete (room)");
    }

    private static async Task<Ok<IEnumerable<SearchRoomGroupResponse>>> GetHandler(ISender sender, [AsParameters] SearchRoomQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PostHandler(ISender sender, CreateRoomCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PutHandler(ISender sender, UpdateRoomCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> DeleteHandler(ISender sender, Guid id) =>
        (await sender.Send(new DeleteRoomCommand(id))).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
