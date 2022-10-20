using System;
using Library.Application.DTO.Book;
using Library.Application.DTO.User;
using Library.Domain.Entities;

namespace Library.Api.ViewModels;

public class AllBooksVM
{
    public List<BookDto> Books { get; set; }
    public UserDto UserDto { get; set; }
}

