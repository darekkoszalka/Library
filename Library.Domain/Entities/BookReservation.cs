using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

public class BookReservation
{
    
    public int Id { get; set; }

    public DateTime ReservationDate { get; set; }

    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser ApplicationUser { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public Book Book { get; set; }
}

