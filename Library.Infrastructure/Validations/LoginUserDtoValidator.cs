using System;
using FluentValidation;
using Library.Application.DTO.User;

namespace Library.Infrastructure.Validations;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Wypełnij pole 'email'")
            .EmailAddress()
            .WithMessage("Wpisz prawidłowy adres email");
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Podaj hasło");
    }
}

