namespace CloudHotel.Application.Reservations.CreateReservation;

public sealed class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.RoomDetails)
            .NotEmpty()
            .WithMessage("O quarto não pode ser vazio")
            .WithErrorCode("CreateReservationCommand.EmptyRoom")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.GuestName)
            .NotEmpty()
            .WithMessage("O nome do hóspede não pode ser vazio")
            .WithErrorCode("CreateReservationCommand.EmptyGuestName")
            .WithSeverity(Severity.Warning);
    }
}