using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTO.User;

public class LoginUserDto
{
    public string Email { get; set; }

    [Display(Name = "Hasło")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

