using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Api.Models;
using Library.Application.DTO.User;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Library.Api.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly LibraryDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(IUserService userService, LibraryDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {

            _userService = userService;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            TempData["Title"] = "Zarejestruj się";
            StatusOperationViewModel statusOperation = new();

            RegisterUserDtoValidator validator = new(_context);
            var validateResult = validator.Validate(registerUserDto);

            if (validateResult.IsValid)
            {
                var result = await _userService.Register(registerUserDto);

                if (result)
                {
                    statusOperation.Status = true;
                    statusOperation.Message = "Rejestracja przebiegła pomyślnie";
                    return RedirectToAction("ConfirmOperation", statusOperation);
                }


                statusOperation.Status = false;
                statusOperation.Message = "Wystąpił błąd rejestracji, spróbuj ponownie.";

                return RedirectToAction("ConfirmOperation", statusOperation);
            }

            foreach (var failure in validateResult.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }
            return View();

        }

        public IActionResult Login()
        {
            TempData["Title"] = "Zaloguj się";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            LoginUserDtoValidator validator = new();
            var validateResult = validator.Validate(loginUserDto);

            if (validateResult.IsValid)
            {
                var result = await _userService.Login(loginUserDto);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Niepoprawny login lub hasło.");
                return View();

            }
            foreach (var failure in validateResult.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            StatusOperationViewModel statusOperation = new()
            {
                Status = true,
                Message = "Zostałeś wylogowany"
            };

            return RedirectToAction("ConfirmOperation", statusOperation);
    }

    public IActionResult ConfirmOperation(StatusOperationViewModel statusOperation)
    {

        return View(statusOperation);
    }


}
}

