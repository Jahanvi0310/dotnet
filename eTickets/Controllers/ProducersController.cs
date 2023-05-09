using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using eTickets.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Data;
namespace eTickets.Controllers
{
    public class ProducersController:Controller
    {
private readonly AppDbContext _context;
public ProducersController(AppDbContext context)
{
    _context=context;
}
public async Task <IActionResult> Index(){
    var allProducers= await _context.Producers!.ToListAsync();
    return View();
}
    }
}