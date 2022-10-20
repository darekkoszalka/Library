using System;
using Library.Application.DTO.Book;
using Library.Domain.Entities;

namespace Library.Application.IServices;

public interface IBookService
{
    Task<List<BookDto>> GetAll();
    Task<int> Create(CreateBookDto newBook);
    Task Update(BookDto bookDto);
    Task Delete(int bookId);
    Task<BookDto> GetBookById(int bookId);
    Task<bool> BookExist(int bookId);
}

