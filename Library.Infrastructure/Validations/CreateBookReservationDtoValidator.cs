using System;
using FluentValidation;
using Library.Application.DTO.BookReservation;

namespace Library.Infrastructure.Validations;

public class CreateBookReservationDtoValidator : AbstractValidator<CreateBookReservationDto>
{
    public CreateBookReservationDtoValidator()
    {
        RuleFor(r => r.BookId)
            .NotEmpty();

        RuleFor(r => r.UserId)
            .NotNull()
            .NotEmpty();

    }
}

