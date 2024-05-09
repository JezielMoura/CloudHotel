using CloudHotel.Domain.SettingsAggregate;
using CloudHotel.Infrastructure.Persistence.Context;

namespace CloudHotel.Infrastructure.Persistence.Repositories;

internal sealed class SettingsRepository(AppDbContext dbContext) : ISettingsRepository
{
    public async Task<Settings?> Get() =>
        await dbContext.Settings.FirstOrDefaultAsync();
        
    public async Task Update(Settings settings)
    {
        if (await dbContext.Settings.AnyAsync())
        {
            dbContext.Settings.Update(settings);
            return;
        }

        await dbContext.Settings.AddAsync(settings);
    }
}