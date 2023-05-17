using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using eTickets.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Data;
using eTickets.Data.Services;
namespace eTickets.Controllers
{
    public class CinemasController:Controller
    {
  private readonly ICinemasService _service;
public CinemasController(ICinemasService service)
{
    _service=service;
}
public async Task<IActionResult> Index()
{
    var cinemas = await _service.GetAllAsync();
    return View(cinemas);
}
        //GET://Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")] Cinema cinema)
{
    if (!ModelState.IsValid)
    {
        return View(cinema);
    }

     await _service.AddAsync(cinema);

    return RedirectToAction(nameof(Index));
}
public async Task<IActionResult> Details(int id)
{
    var Cinema = await _service.GetByIdAsync(id);

    if (Cinema == null)
    {
        return RedirectToAction("NotFound", "Cinemas");
    }

    return View(Cinema);
}
public async Task<IActionResult> Delete(int id)
{
    var Cinema = await _service.GetByIdAsync(id);

    if (Cinema == null)
    {
        return NotFound();
    }

    return View(Cinema);
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    await _service.DeleteAsync(id);
    return RedirectToAction(nameof(Index));
}
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
public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureUrl, Bio")] Cinema Cinema)
{
    if (id != Cinema.Id)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(Cinema);
    }

    try
    {
        await _service.UpdateAsync(id, Cinema);
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