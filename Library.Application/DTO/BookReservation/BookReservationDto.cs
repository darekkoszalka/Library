using System;
using Library.Domain.Entities;

namespace Library.Application.DTO.BookReservation;

public class BookReservationDto
{
    public int Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public Library.Domain.Entities.Book Book{ get; set; }
    public DateTime ReservationDate { get; set; }
}

