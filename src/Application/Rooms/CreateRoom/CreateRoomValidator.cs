namespace CloudHotel.Application.Rooms.CreateRoom;

public sealed class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
{
    public const int NameMinimumLenght = 3;
    public const int NameMaximumLenght = 45;
    public const int DescriptionMinimumLenght = 3;
    public const int DescriptionMaximumLenght = 120;

    public CreateRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")
            .WithErrorCode("CreateRoomCommand.EmptyName")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Name)
            .Length(NameMinimumLenght, NameMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 45 caracteres")
            .WithErrorCode("CreateRoomCommand.NameLenght")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia")
            .WithErrorCode("CreateRoomCommand.EmptyDescription")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Description)
            .Length(DescriptionMinimumLenght, DescriptionMaximumLenght)
            .WithMessage("A descrição deve ter entre 3 e 45 caracteres")
            .WithErrorCode("CreateRoomCommand.DescriptionLenght")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("O código não pode ser vazio")
            .WithErrorCode("CreateRoomCommand.EmptyCode")
            .WithSeverity(Severity.Warning);
    }

}
