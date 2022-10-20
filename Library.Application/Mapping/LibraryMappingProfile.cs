using System;
using AutoMapper;
using Library.Application.DTO.Book;
using Library.Application.DTO.BookReservation;
using Library.Application.DTO.User;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class LibraryMappingProfile : Profile
{
    public LibraryMappingProfile()
    {
        //Book mapping
        CreateMap<Book, BookDto>();
        CreateMap<BookDto, UpdateBookDto>();
        CreateMap<BookDto, Book>();
        CreateMap<CreateBookDto, Book>();

        //user mapping

        CreateMap<RegisterUserDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>();

        //book reservation mapping
        CreateMap<BookReservation, CreateBookReservationDto>();
        CreateMap<CreateBookReservationDto, BookReservation>();
        CreateMap<BookReservation, BookReservationDto>();
        
    }
}

