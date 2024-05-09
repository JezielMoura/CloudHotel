using CloudHotel.Application.Settings.GetSettings;
using CloudHotel.Application.Settings.UpdateSettings;

namespace CloudHotel.Infrastructure.Endpoints;

public static class SettingsEndpoints
{
    public static void MapSettingsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGet("/", GetHandler).WithName("Get (settings)");
        group.MapPost("/", PostHandler).WithName("Add (settings)");
    }

    private static async Task<Ok<GetSettingsResponse>>  GetHandler(ISender sender, [AsParameters] GetSettingsQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PostHandler(ISender sender, UpdateSettingsCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
