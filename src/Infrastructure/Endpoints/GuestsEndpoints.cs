using CloudHotel.Application.Guests.CreateGuest;
using CloudHotel.Application.Guests.GetGuest;
using CloudHotel.Application.Guests.SearchGuest;

namespace CloudHotel.Infrastructure.Endpoints;

public static class GuestsEndpoints
{
    public static void MapGuestsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGet("/", SearchHandler).WithName("Search (guest)");
        group.MapGet("/{id}", GetHandler).WithName("Get (guest)");
        group.MapPost("/", PostHandler).Validate<CreateGuestCommand>().WithName("Add (guest)");
    }

    private static async Task<Ok<IEnumerable<SearchGuestResponse>>> SearchHandler(ISender sender, [AsParameters] SearchGuestQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Results<Ok<GetGuestResponse>, NotFound<string>>>  GetHandler(ISender sender, [AsParameters] GetGuestQuery query) =>
        (await sender.Send(query)).Match<Results<Ok<GetGuestResponse>, NotFound<string>>>((data) => Ok(data), (error) => NotFound(error.Title));

    private static async Task<Results<Ok<Guid>, BadRequest<Error>>> PostHandler(ISender sender, CreateGuestCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<Guid>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
