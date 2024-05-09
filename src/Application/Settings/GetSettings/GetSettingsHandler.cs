using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Settings.GetSettings;

internal sealed class GetSettingsHandler : IRequestHandler<GetSettingsQuery, GetSettingsResponse>
{
    private readonly IAppDbContext _appDbContext;

    public GetSettingsHandler(IAppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<GetSettingsResponse> Handle(GetSettingsQuery query, CancellationToken cancellationToken)
    {
        var settings = await _appDbContext.Settings.FirstOrDefaultAsync(cancellationToken);

        if (settings is null)
            return GetSettingsResponse.Create(SettingsModel.Default);

        return GetSettingsResponse.Create(settings);
    }
}
