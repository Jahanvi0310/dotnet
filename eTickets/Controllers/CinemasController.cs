using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using  eTickets.Models;
using eTickets.Data;
namespace eTickets.Controllers
{
    public class CinemasController:Controller
    {
        private readonly AppDbContext _context;
public CinemasController(AppDbContext context)
{
    _context=context;
}
        public async Task <IActionResult> Index()
        {
            //to show the data on index page
            var data = await _context.Cinemas!.ToListAsync();
            return View(data);
        }
    }
}
