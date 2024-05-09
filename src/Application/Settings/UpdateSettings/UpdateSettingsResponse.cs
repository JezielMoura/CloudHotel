using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Settings.UpdateSettings;

public sealed record UpdateSettingsResponse (
    string PropertyName,
    string PropertyEmail,
    string PropertyPhone,
    string PropertyDocument,
    string PropertyImage)
{
    public static UpdateSettingsResponse Create(SettingsModel settings) =>
        new (settings.Property.Name, settings.Property.Email, settings.Property.Phone, settings.Property.Document, ByteToBase64(settings.Property.Image));

    public static string ByteToBase64(byte[] bytes) =>
        Convert.ToBase64String(bytes);
}