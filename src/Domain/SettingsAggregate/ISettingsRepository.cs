namespace CloudHotel.Domain.SettingsAggregate;

public interface ISettingsRepository
{
    Task Update(Settings settings);
    Task<Settings?> Get();
}