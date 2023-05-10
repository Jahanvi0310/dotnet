using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using  eTickets.Models;
using eTickets.Data;
namespace eTickets.Controllers
{
    public class ActorsController:Controller
    {
        private readonly AppDbContext _context;
public ActorsController(AppDbContext context)
{
    _context=context;
}
        public IActionResult Index()
        {
            //to show the data on index page
            var data = _context.Actors!.ToList();
            return View(data);
        }
    }
}
