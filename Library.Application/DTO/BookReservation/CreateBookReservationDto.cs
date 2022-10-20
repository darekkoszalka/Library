using System;
namespace Library.Application.DTO.BookReservation;

public class CreateBookReservationDto
{
    public string UserId { get; set; }
    public int BookId { get; set; }
    public DateTime ReservationDate { get; set; }

}

