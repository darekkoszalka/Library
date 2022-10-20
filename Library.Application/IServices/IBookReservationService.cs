using System;
using Library.Application.DTO.BookReservation;

namespace Library.Application.IServices;

public interface IBookReservationService
{
    Task<int> Create(CreateBookReservationDto createReservation);
    Task<List<BookReservationDto>> GetAll();
    Task<List<BookReservationDto>> GetBookReservations(int bookId);
    Task<List<BookReservationDto>> GetUserReservations(string userId);
    Task Delete(int bookId);
    Task<bool> BookReservationExists(int reservationId);
}

