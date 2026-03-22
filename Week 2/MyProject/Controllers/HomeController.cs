using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;

namespace MyProject.Controllers;

/// <summary>
/// This controller handles basic pages like Home, Privacy, and About.
/// It inherits from Controller, providing functionality for web responses.
/// </summary>
public class HomeController : Controller
{

    public string? Name { get; set; }
    // The Index action is the entry point for the home page.
    // When a user navigates to '/' or '/Home/Index', this method is executed.
    public IActionResult Index()
    {
        // View() tells the app to find and render the matching view file: 'Views/Home/Index.cshtml'.
        return View();
    }

    // Handles requests for '/Home/Privacy'.
    public IActionResult Privacy()
    {
        return View();
    }

    // Handles requests for '/Home/AboutMe'.
    public IActionResult AboutMe()
    {
        return View();
    }

    // This method handles unexpected errors.
    // It configures response caching to avoid storing error pages.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Passes an ErrorViewModel containing the request ID for diagnostics.
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
