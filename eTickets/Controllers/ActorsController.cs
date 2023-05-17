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
            var data =await _service.GetAllAsync();
          
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

     await _service.AddAsync(actor);

    return RedirectToAction(nameof(Index));
}
public async Task<IActionResult> Details(int id)
{
    var actor = await _service.GetByIdAsync(id);

    if (actor == null)
    {
        return RedirectToAction("NotFound", "Actors");
    }

    return View(actor);
}

// GET: Actors/Delete/{id}
public async Task<IActionResult> Delete(int id)
{
    var actor = await _service.GetByIdAsync(id);

    if (actor == null)
    {
        return NotFound();
    }

    return View(actor);
}

// POST: Actors/Delete/{id}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    await _service.DeleteAsync(id);
    return RedirectToAction(nameof(Index));
}
// GET: Actors/Edit/{id}
public async Task<IActionResult> Edit(int id)
{
    var actor = await _service.GetByIdAsync(id);

    if (actor == null)
    {
        return View("NotFound");
    }

    return View(actor);
}

// POST: Actors/Edit/{id}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureUrl, Bio")] Actor actor)
{
    if (id != actor.Id)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(actor);
    }

    try
    {
        await _service.UpdateAsync(id, actor);
    }
    catch (Exception)
    {
        return NotFound();
    }

    return RedirectToAction(nameof(Index));
}
public new IActionResult NotFound()
{
    return View();
}



    }
}
