using System;
using AutoMapper;
using Library.Application.DTO.User;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(LibraryDbContext context,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Login(LoginUserDto loginUserDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, loginUserDto.RememberMe,lockoutOnFailure: false);

        if(result.Succeeded)
        {
            return true;
        }

        return false;

    }

    public async Task<bool> Register(RegisterUserDto registerUserDto)
    {
        ApplicationUser user = new()
        {
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Email = registerUserDto.Email,
            EmailConfirmed = true,
            UserName = registerUserDto.Email
            
        };
       
       var result =  await _userManager.CreateAsync(user, registerUserDto.Password);
        if (result.Succeeded) return true;

        return false;      
    }

    public async Task<bool> UserExists(string email)
    {
        var userExist = await _userManager.FindByEmailAsync(email);
        if (userExist is null) return false;
        return true;
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Email == email);
        return _mapper.Map<UserDto>(user);
    }


}

