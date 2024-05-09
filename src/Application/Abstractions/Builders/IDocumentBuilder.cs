using CloudHotel.Application.Reservations.GetReservation;
using CloudHotel.Application.Settings.GetSettings;

namespace CloudHotel.Application.Abstractions.Builders;

public interface IDocumentBuilder
{
    Task<string> Build(GetReservationResponse response, GetSettingsResponse settingsResponse);
}