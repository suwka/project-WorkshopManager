using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkshopManager.Models;

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

    public IActionResult AddVehicle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddVehicle(string marka, string model, int rokProdukcji, string coNaprawic)
    {
        var vehicle = new Vehicle
        {
            Marka = marka,
            Model = model,
            RokProdukcji = rokProdukcji,
            CoNaprawic = coNaprawic
        };
        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
        return RedirectToAction("VehicleList");
    }

    public IActionResult VehicleList()
    {
        return View(_context.Vehicles.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

