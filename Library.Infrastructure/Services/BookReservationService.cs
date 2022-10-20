using System;
using System.Net;
using AutoMapper;
using Library.Application.DTO.BookReservation;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services;

public class BookReservationService : IBookReservationService
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BookReservationService(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> BookReservationExists(int reservationId)
    {
        return await _context.BookReservations
            .Where(r=>r.Id == reservationId)
            .AnyAsync();
    }

    public async Task<int> Create(CreateBookReservationDto newReservation)
    {
        var reservation = _mapper.Map<BookReservation>(newReservation);

        await _context.BookReservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return reservation.Id;

    }

    public async Task Delete(int reservationId)
    {
        var bookReservation = await _context.BookReservations
            .FirstOrDefaultAsync(r => r.Id == reservationId);
        if(bookReservation is not null)
        {
            _context.BookReservations.Remove(bookReservation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<BookReservationDto>> GetAll()
    {
        var reservations = await _context.BookReservations
            .Include(r => r.ApplicationUser)
            .Include(r => r.Book)
            .AsNoTracking()
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();
        return _mapper.Map<List<BookReservationDto>>(reservations);
    }

    public async Task<List<BookReservationDto>> GetBookReservations(int bookId)
    {
        var reservations = await _context.BookReservations
            .AsNoTracking()
            .Where(r => r.BookId == bookId)
            .Include(r => r.ApplicationUser)
            .Include(r => r.Book)
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();
        return _mapper.Map<List<BookReservationDto>>(reservations);
    }

    public async Task<List<BookReservationDto>> GetUserReservations(string userId)
    {
        var reservations = await _context.BookReservations
        .AsNoTracking()
            .Where(r => r.UserId == userId)
            .Include(r => r.ApplicationUser)
            .Include(r => r.Book)
            .OrderByDescending(r => r.ReservationDate)
            .ToListAsync();

        return _mapper.Map<List<BookReservationDto>>(reservations);
    }
}

