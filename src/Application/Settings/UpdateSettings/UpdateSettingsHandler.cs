using CloudHotel.Domain.SettingsAggregate;

namespace CloudHotel.Application.Settings.UpdateSettings;

internal sealed class UpdateSettingsHandler : IRequestHandler<UpdateSettingsCommand, Result<bool, Error>>
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSettingsHandler(ISettingsRepository SettingsRepository, IUnitOfWork unitOfWork) =>
        (_settingsRepository, _unitOfWork) = (SettingsRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(UpdateSettingsCommand command, CancellationToken cancellationToken)
    {
        var settings = command.MapToSettings();
        await _settingsRepository.Update(settings);
        
        return await _unitOfWork.Commit();
    }
}
