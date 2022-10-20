using System;
using Library.Application.DTO.User;

namespace Library.Application.IServices;

public interface IUserService
{
    Task<bool> Register(RegisterUserDto registerUserDto);
    Task<bool> Login(LoginUserDto loginUserDto);
    Task<bool> UserExists(string email);
    Task<UserDto> GetUserByEmail(string email);
}

