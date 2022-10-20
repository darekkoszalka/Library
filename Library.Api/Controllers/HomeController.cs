using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Api.Models;

namespace Library.Api.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }



    
}

