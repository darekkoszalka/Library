using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTO.User;

public class UserDto
{
    public string Id { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Imię")]
    public string FirstName { get; set; }

    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }

}

