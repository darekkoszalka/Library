using System;
using FluentValidation;
using Library.Application.DTO.Book;

namespace Library.Infrastructure.Validations;

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .WithMessage("Pole 'Tytuł' jest wymagane");
        RuleFor(b => b.Author)
            .NotEmpty()
            .WithMessage("Pole 'Autor' jest wymagane");
        RuleFor(b => b.PublishDate)
            .NotEmpty()
            .WithMessage("Pole 'Data pulikacjj' jest wymagane");
        RuleFor(b => b.PublishDate)
            .Custom((value, context) =>
            {
                if (value > DateTime.Now)
                {
                    context.AddFailure("Data nie może być z przyszłości");
                }
            }
            );
        RuleFor(b => b.Description)
            .NotEmpty()
            .WithMessage("Dodaj opis");
    }
}

