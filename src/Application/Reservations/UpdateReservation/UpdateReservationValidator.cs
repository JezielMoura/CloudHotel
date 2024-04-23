namespace CloudHotel.Application.Reservations.UpdateReservation;

public sealed class UpdateReservationValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O id da reserva não pode ser vazia")
            .WithErrorCode("UpdateReservationCommand.EmptyId")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.RoomCode)
            .NotEmpty()
            .WithMessage("O código do quarto não pode ser vazio")
            .WithErrorCode("UpdateReservationCommand.EmptyRoomCode")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("O id do quarto não pode ser vazio")
            .WithErrorCode("UpdateReservationCommand.EmptyRoomId")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.GuestName)
            .NotEmpty()
            .WithMessage("O nome do hóspede não pode ser vazio")
            .WithErrorCode("UpdateReservationCommand.EmptyGuestName")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.GuestId)
            .NotEmpty()
            .WithMessage("O id do hóspede não pode ser vazio")
            .WithErrorCode("UpdateReservationCommand.EmptyGuestId")
            .WithSeverity(Severity.Warning);
    }
}