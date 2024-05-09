using CloudHotel.Domain.SettingsAggregate;
using SettingsModel = CloudHotel.Domain.SettingsAggregate.Settings;

namespace CloudHotel.Application.Settings.UpdateSettings;

public sealed record UpdateSettingsCommand (
    Guid Id,
    string PropertyName,
    string PropertyEmail,
    string PropertyPhone,
    string PropertyDocument,
    string PropertyImage
) : IRequest<Result<bool, Error>>
{
    public SettingsModel MapToSettings() =>
        new (Id, new PropertyDetails(PropertyName, PropertyEmail, PropertyPhone, PropertyDocument, Base64ToBytes(PropertyImage)));
    
    public static byte[] Base64ToBytes(string image) =>
        Convert.FromBase64String(image);
}
