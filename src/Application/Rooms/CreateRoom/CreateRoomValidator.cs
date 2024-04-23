namespace CloudHotel.Application.Guests.CreateGuest;

public sealed class CreateGuestValidator : AbstractValidator<CreateGuestCommand>
{
    public const int NameMinimumLenght = 3;
    public const int NameMaximumLenght = 120;
    public const int EmailMinimumLenght = 3;
    public const int EmailMaximumLenght = 120;
    public const int PhoneMinimumLenght = 3;
    public const int PhoneMaximumLenght = 120;

    public CreateGuestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")
            .WithErrorCode("CreateGuestCommand.EmptyName")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Name)
            .Length(NameMinimumLenght, NameMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 120 caracteres")
            .WithErrorCode("CreateGuestCommand.NameLenght")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia")
            .WithErrorCode("CreateGuestCommand.EmptyEmail")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Email)
            .Length(EmailMinimumLenght, EmailMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 120 caracteres")
            .WithErrorCode("CreateGuestCommand.EmailLenght")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia")
            .WithErrorCode("CreateGuestCommand.EmptyPhone")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.Phone)
            .Length(PhoneMinimumLenght, PhoneMaximumLenght)
            .WithMessage("O nome deve ter entre 3 e 120 caracteres")
            .WithErrorCode("CreateGuestCommand.PhoneLenght")
            .WithSeverity(Severity.Warning);
    }
}