using System;
using FluentValidation;
using Library.Application.DTO.Book;
using Library.Application.DTO.User;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Services;
using Library.Infrastructure.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<LibraryDbContext>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookReservationService, BookReservationService>();


        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
        services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
        services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();

        return services;
    }
}

