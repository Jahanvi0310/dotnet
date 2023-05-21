using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using  eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Data.Services;
namespace eTickets.Controllers
{
    public class MoviesController:Controller
    {
       private readonly IMoviesService _service;
public MoviesController(IMoviesService service)
{
    _service = service;
}
public async Task<IActionResult> Index()
{
    var data = await _service.GetAllAsync(n => n.Cinema!);

    if (data != null)
    {
        return View(data);
    }
    else
    {
        return NotFound();
    }
}
public async Task<IActionResult> Details(int id)
{
    var movie = await _service.GetMovieByIdAsync(id);
    if (movie == null)
    {
        return NotFound();
    }

    return View(movie);
}
#pragma warning disable CS1998

public async Task<IActionResult> Create()
{
    return View();
}


 
    }
}
