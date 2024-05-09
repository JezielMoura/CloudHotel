using CloudHotel.Application.Reservations.CreateReservation;
using CloudHotel.Application.Reservations.DeleteReservation;
using CloudHotel.Application.Reservations.FnrhReservation;
using CloudHotel.Application.Reservations.GetCalendar;
using CloudHotel.Application.Reservations.GetReservation;
using CloudHotel.Application.Reservations.SearchReservation;
using CloudHotel.Application.Reservations.UpdateReservation;

namespace CloudHotel.Infrastructure.Endpoints;

public static class ReservationsEndpoints
{
    public static void MapReservationsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapGet("/{id:guid}", GetHandler).WithName("Get (reservation)");
        group.MapGet("/", SearchHandler).WithName("Search (reservation)");
        group.MapGet("/fnrh/{id:guid}", GetFnrh).WithName("Get FNRH (reservation)");
        group.MapGet("/calendar", CalendarHandler).WithName("Calendar (reservation)");
        group.MapGet("/summary", SummaryHandler).WithName("Summary (reservation)");
        group.MapPost("/", PostHandler).Validate<CreateReservationCommand>().WithName("Add (reservation)");
        group.MapPut("/", PutHandler).Validate<UpdateReservationCommand>().WithName("Update (reservation)");
        group.MapDelete("/{id:guid}", DeleteHandler).WithName("Delete (reservation)");
    }

    private static async Task<Results<Ok<GetReservationResponse>, NotFound<string>>> GetHandler(ISender sender, [AsParameters] GetReservationByIdQuery query) =>
        await sender.Send(query) is {} reservation ? Ok(reservation) : NotFound("Reservation not found");

    private static async Task<Results<Ok<string>, NotFound<string>>> GetFnrh(ISender sender, [AsParameters] FnrhReservationQuery query) =>
        await sender.Send(query) is {} reservation ? Ok(reservation) : NotFound("Reservation not found");

    private static async Task<Ok<IEnumerable<SearchReservationResponse>>> SearchHandler(ISender sender, [AsParameters] SearchReservationQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Ok<IEnumerable<GetCalendarResponse>>> CalendarHandler(ISender sender, [AsParameters] GetCalendarQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Ok<GetSummaryResponse>> SummaryHandler(ISender sender, [AsParameters] GetSummaryQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Results<Ok<Guid>, BadRequest<Error>>> PostHandler(ISender sender, CreateReservationCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<Guid>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PutHandler(ISender sender, UpdateReservationCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> DeleteHandler(ISender sender, [AsParameters] DeleteReservationCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
