using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Settings.GetSettings;

public sealed record GetSettingsResponse (
    Guid Id,
    string PropertyName,
    string PropertyEmail,
    string PropertyPhone,
    string PropertyDocument,
    string PropertyImage)
{
    public const string EmptyImage = "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

    public static GetSettingsResponse Create(SettingsModel settings) =>
        new (settings.Id, settings.Property.Name, settings.Property.Email, settings.Property.Phone, settings.Property.Document, ByteToBase64(settings.Property.Image));

    public static string ByteToBase64(byte[] bytes) =>
        bytes.Length != 0 ? Convert.ToBase64String(bytes) : EmptyImage;
}