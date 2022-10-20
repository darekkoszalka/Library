using System;
using FluentValidation;
using Library.Application.DTO.User;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Validations
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly LibraryDbContext _context;

        public RegisterUserDtoValidator(LibraryDbContext context)
        {
            _context = context;
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Wpisz poprawny adres email.");
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("Pole 'Imię' jest wymagane");
            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("Pole 'Nazwisko' jest wymagane");

            RuleFor(x => x.Password)
                .MinimumLength(6);
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("Hasła nie są takie same");
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = _context.ApplicationUsers.Any(u => u.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Email", "Ten email jest już używany");
                    }
                }

                );
            
        }
    }
}

