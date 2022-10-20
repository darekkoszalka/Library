using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTO.User;

public class RegisterUserDto
{

    public string Email { get; set; }

    [Display(Name ="Imię")]
    public string FirstName { get; set; }

    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }

    [Display(Name ="Hasło")]
    public string Password { get; set; }

    [Display(Name = "Powtórz hasło")]
    public string ConfirmPassword { get; set; }
}

