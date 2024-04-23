namespace CloudHotel.Application.Rooms.UpdateRoom;

public sealed class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
{
    public const int NameMinimumLenght = 3;
    public const int NameMaximumLenght = 120;
    public const int DescriptionMinimumLenght = 3;
    public const int DescriptionMaximumLenght = 120;

    public UpdateRoomValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O id não pode ser vazio")
            .WithErrorCode("UpdateRoomCommand.EmptyId")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")
            .WithErrorCode("UpdateRoomCommand.EmptyName")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Name)
            .Length(NameMinimumLenght, NameMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 120 caracteres")
            .WithErrorCode("UpdateRoomCommand.NameLenght")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia")
            .WithErrorCode("UpdateRoomCommand.EmptyDescription")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Description)
            .Length(DescriptionMinimumLenght, DescriptionMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 120 caracteres")
            .WithErrorCode("UpdateRoomCommand.DescriptionLenght")
            .WithSeverity(Severity.Warning);
    }
}