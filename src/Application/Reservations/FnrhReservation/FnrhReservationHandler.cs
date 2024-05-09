using CloudHotel.Application.Abstractions.Builders;
using CloudHotel.Application.Reservations.GetReservation;
using CloudHotel.Application.Settings.GetSettings;
using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.SettingsAggregate;
using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Reservations.FnrhReservation;

internal sealed class FnrhReservationHandler : IRequestHandler<FnrhReservationQuery, string?>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IDocumentBuilder _documentBuilder;
    private readonly ISettingsRepository _settingsRepository;

    public FnrhReservationHandler(
        IReservationRepository reservationRepository,
        IGuestRepository guestRepository,
        IDocumentBuilder documentBuilder,
        ISettingsRepository settingsRepository)
    {
        _reservationRepository = reservationRepository;
        _guestRepository = guestRepository;
        _documentBuilder = documentBuilder;
        _settingsRepository = settingsRepository;
    }

    public async Task<string?> Handle(FnrhReservationQuery query, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.Get(query.Id);
        var settings = await _settingsRepository.Get() ?? SettingsModel.Default;

        if (reservation is null)
            return null;

        var guest = await _guestRepository.Get(reservation.Guest.Id);
        var reservationResponse = GetReservationResponse.Create(reservation, guest!);
        var settingsResponse = GetSettingsResponse.Create(settings);
        var htmlString = await _documentBuilder.Build(reservationResponse, settingsResponse);

        return htmlString;
    }
}
