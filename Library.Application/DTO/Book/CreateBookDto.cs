using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Application.DTO.Book;

public class CreateBookDto
{
    [Display(Name = "Tytuł")]
    public string Title { get; set; }

    [Display(Name = "Autor")]
    public string Author { get; set; }

    [Display(Name = "Data publikacji")]
    public DateTime PublishDate { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }
}

