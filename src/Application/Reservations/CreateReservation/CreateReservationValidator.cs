namespace CloudHotel.Application.Reservations.CreateReservation;

public sealed class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.RoomCode)
            .NotEmpty()
            .WithMessage("O quarto não pode ser vazio")
            .WithErrorCode("CreateReservationCommand.EmptyRoom")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.GuestName)
            .NotEmpty()
            .WithMessage("O nome do hóspede não pode ser vazio")
            .WithErrorCode("CreateReservationCommand.EmptyGuestName")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Departure)
            .Must((command, departure) => departure > command.Arrival)
            .WithMessage("A data do check-out deve ser maior que o check-in")
            .WithErrorCode("CreateReservationCommand.CheckoutLessOrEqualThanCheckin")
            .WithSeverity(Severity.Warning);
    }
}