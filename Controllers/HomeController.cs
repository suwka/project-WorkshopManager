using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkshopManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkshopManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WorkshopContext _context;

    public HomeController(ILogger<HomeController> logger, WorkshopContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Video()
    {
        ViewBag.Message = "jebac barcelone";
        ViewBag.ShowVideo = true;
        return View("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AddVehicle()
    {
        var userEmails = _context.Users.Select(u => u.Email).ToList();
        ViewBag.UserEmails = userEmails;
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddVehicle(string marka, string model, int rokProdukcji, string coNaprawic, string userEmail)
    {
        var vehicle = new Vehicle
        {
            Marka = marka,
            Model = model,
            RokProdukcji = rokProdukcji,
            CoNaprawic = coNaprawic,
            UserEmail = userEmail
        };
        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
        return RedirectToAction("VehicleList");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult VehicleList()
    {
        var vehicles = _context.Vehicles.ToList();
        return View(vehicles);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult VehicleDetails(int id)
    {
        var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle == null)
            return NotFound();
        var user = _context.Users.FirstOrDefault(u => u.Email == vehicle.UserEmail);
        ViewBag.User = user;
        return View(vehicle);
    }

    [Authorize]
    public IActionResult MyVehicles()
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
        var vehicles = _context.Vehicles.Where(v => v.UserEmail == userEmail).ToList();
        return View(vehicles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult UserList()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult EditUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        return View(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult EditUser(int id, string firstName, string lastName, string email, string password, string role)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        user.FirstName = firstName;
        user.LastName = lastName;
        user.Email = email;
        if (!string.IsNullOrEmpty(password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        user.Role = role;
        _context.SaveChanges();
        return RedirectToAction("UserList");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AddUser()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddUser(string firstName, string lastName, string email, string password, string role)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("UserList");
    }
}
