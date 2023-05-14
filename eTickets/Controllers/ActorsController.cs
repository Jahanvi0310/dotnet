using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using  eTickets.Models;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;
namespace eTickets.Controllers
{
    public class ActorsController:Controller
    {
        private readonly IActorsService _service;
public ActorsController(IActorsService service)
{
    _service=service;
}
        public async Task<IActionResult> Index()
        {
            //to show the data on index page
            var data =await _service.getAll();
            return View(data);
        }
        //url for creating the actor 
        //GET://Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")] Actor actor)
{
    if (!ModelState.IsValid)
    {
        return View(actor);
    }

     _service.Add(actor);

    return RedirectToAction(nameof(Index));
}

    }
}
