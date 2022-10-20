using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Api.ViewModels;
using Library.Application.DTO.Book;
using Library.Application.DTO.User;
using Library.Application.IServices;
using Library.Domain.Entities;
using Library.Infrastructure.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService,
            UserManager<ApplicationUser> userManager,
            IUserService userService, IMapper mapper)
        {
            _bookService = bookService;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<IActionResult> GetAllBooks()
        {
            TempData["Title"] = "Lista książek";
            var books = await _bookService.GetAll();

            return View(books);
        }

        public IActionResult Create()
        {
            TempData["Title"] = "Dodaj książkę";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookDto createBookDto)
        {
            CreateBookDtoValidator validator = new();
            var result = validator.Validate(createBookDto);

            if (result.IsValid)
            {
                await _bookService.Create(createBookDto);
                TempData["Status"] = $"Pomyślnie dodano książkę";
                return RedirectToAction("GetAllBooks");
            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View();
            }

        }

        public async Task<IActionResult> Update(int bookId)
        {
            TempData["Title"] = "Edytuj książke";
            var book = await _bookService.GetBookById(bookId);
            if (book is null)
            {
                return NotFound();
            }
            TempData["Status"] = $"Pomyślnie zaktualizowano książkę: {book.Title}";
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookDto bookDto)
        {
            BookDtoValidator validator = new();
            var result = validator.Validate(bookDto);

            if (result.IsValid)
            {
                await _bookService.Update(bookDto);
                return RedirectToAction("GetAllBooks");
            }

            foreach (var failure in result.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }
            return View();


        }

        public async Task<IActionResult> Details(int bookId)
        {
            TempData["Title"] = "Szczegóły książki";
            var bookDto = await _bookService.GetBookById(bookId);
            return View(bookDto);
        }



        public async Task<IActionResult> Delete(int bookId)
        {

            var book = await _bookService.GetBookById(bookId);
            if (book is null)
            {
                return NotFound();
            }

            await _bookService.Delete(bookId);
            TempData["Status"] = "Pomyślnie usunięto książkę";

            return RedirectToAction("GetAllBooks");
        }

    }
}

