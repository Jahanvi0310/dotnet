using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using  eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
namespace eTickets.Controllers
{
    public class MoviesController:Controller
    {
        private readonly AppDbContext _context;
public MoviesController(AppDbContext context)
{
    _context=context;
}
        public async Task <IActionResult> Index()
        {
            //to show the data on index page
            var data = await _context.Movies!.ToListAsync();
            return View();
        }
    }
}
