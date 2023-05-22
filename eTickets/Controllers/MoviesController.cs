using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using  eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    var movieDropdownsData=await _service.GetNewMovieDropdownsValues();
    ViewBag.Cinemas=new SelectList(movieDropdownsData.Cinemas,"Id","Name");
    ViewBag.Producers=new SelectList(movieDropdownsData.Producers,"Id","FullName");
    ViewBag.ActorIds=new SelectList(movieDropdownsData.Actors,"Id","FullName");
    return View();
}
[HttpPost]
public async Task<IActionResult> Create(NewMovieVM newMovie)
{
    if (ModelState.IsValid)
    {
        var createdMovie = await _service.CreateMovieAsync(newMovie);
        
        if (createdMovie != null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Failed to create the movie. Please try again.");
        }
    }
    
    var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
    ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
    ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
    ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
    
    return View(newMovie);
}

 
    }
}
