
namespace CloudHotel.Application.Guests.GetGuest;

internal sealed class GetGuestsHandler : IRequestHandler<GetGuestQuery, Result<GetGuestResponse, Error>>
{
    private readonly IAppDbContext _appDbContext;

    public GetGuestsHandler(IAppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<Result<GetGuestResponse, Error>> Handle(GetGuestQuery query, CancellationToken cancellationToken)
    {
        var guest = await _appDbContext.Guests.FindAsync(query.Id);

        if (guest is null)
            return new Error(Type: "NotFound", Title: $"Guest {query.Id} not found", StatusCode: 404);

        return GetGuestResponse.Create(guest);
    }
}
