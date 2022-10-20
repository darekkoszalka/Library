using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities;


public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public DateTime PublishDate { get; set; }

    [Required]
    public string Description { get; set; }

    
}

