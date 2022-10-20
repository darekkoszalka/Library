using System;
using AutoMapper;
using Library.Application.DTO.Book;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BookService(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> BookExist(int bookId)
    {
        var result = await _context.Books.AnyAsync(b => b.Id == bookId);
        return result;
    }

    public async Task<int> Create(CreateBookDto newBook)
    { 
            var book = _mapper.Map<Book>(newBook);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;    
    }

    public async Task Delete(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if(book is not null)
        {
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }

    }

    public async Task<List<BookDto>> GetAll()
    {
        var books = await _context.Books
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        return _mapper.Map<List<BookDto>>(books);
    }

    public async Task<BookDto> GetBookById(int bookId)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);
        return _mapper.Map<BookDto>(book);
    }

    public async Task Update(BookDto bookDto)
    {
        
            var bookToUpdate = _mapper.Map<Book>(bookDto);
            _context.Books.Update(bookToUpdate);
        await _context.SaveChangesAsync();
        
    }
}

