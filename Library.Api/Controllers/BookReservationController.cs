using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTO.Book;
using Library.Application.DTO.BookReservation;
using Library.Application.IServices;
using Library.Infrastructure.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    public class BookReservationController : Controller
    {
        private readonly IBookReservationService _bookReservationService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        

        public BookReservationController(IBookReservationService bookReservationService,
            IBookService bookService, IUserService userService)
        {
            _bookReservationService = bookReservationService;
            _bookService = bookService;
            _userService = userService;
        }

        public async Task<IActionResult> GetAll()
        {
            TempData["Title"] = "Lista wszystkich rezerwacji";
            var reservations =await _bookReservationService.GetAll();
            return View(reservations);
        }

        public async Task<IActionResult> GetBookReservations(int bookId)
        {
            var reservations = await _bookReservationService.GetBookReservations(bookId);
            TempData["Title"] = "Lista rezerwacji";
            return View(reservations);
        }


        [Authorize]
        public async Task<IActionResult> Create(int bookId)
        {
            var user = await _userService.GetUserByEmail(User.Identity.Name);
            CreateBookReservationDto reservation = new()
            {
                BookId = bookId,
                UserId = user.Id,
                ReservationDate = DateTime.Now
            };
                await _bookReservationService.Create(reservation);
            TempData["Status"] = "Książka została zarezerwowana";
            

            return RedirectToAction("GetAllBooks", "Book");

        }

        public async Task<IActionResult> Delete(int reservationId)
        {
            var reservationExist = await _bookReservationService.BookReservationExists(reservationId);
            if(reservationExist)
            {
               await _bookReservationService.Delete(reservationId);
                TempData["Status"] = "Pomyślnie usunięto rezerwację";

                return RedirectToAction("GetAllBooks", "Book");
            }
            return NotFound();
        }

        public async Task<IActionResult> UserDeleteReservation(int reservationId)
        {
            var reservationExist = await _bookReservationService.BookReservationExists(reservationId);
            if (reservationExist)
            {
                await _bookReservationService.Delete(reservationId);
                TempData["Status"] = "Pomyślnie usunięto rezerwację";

                return RedirectToAction("GetUserReservations");
            }
            return NotFound();
        }

        public async Task<IActionResult> GetUserReservations()
        {
            TempData["Title"] = "Twoje rezerwacje";
            var user = await _userService.GetUserByEmail(User.Identity.Name);
            var reservations = await _bookReservationService.GetUserReservations(user.Id);
            return View(reservations);
        }
    }
}

