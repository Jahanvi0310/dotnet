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
    _service = service;
}
public async Task<IActionResult> Index()
        {
            //to show the data on index page
            var data =await _service.GetAllAsync();
            return View(data);
        }
        //GET://Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
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
    var cinema= await _service.GetByIdAsync(id);

    if (cinema == null)
    {
        return RedirectToAction("NotFound", "Cinemas");
    }

    return View(cinema);
}
public async Task<IActionResult> Delete(int id)
{
    var cinema = await _service.GetByIdAsync(id);

    if (cinema == null)
    {
        return NotFound();
    }

    return View(cinema);
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
    var cinema = await _service.GetByIdAsync(id);

    if (cinema == null)
    {
        return View("NotFound");
    }

    return View(cinema);
}

// POST: Actors/Edit/{id}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
{
    if (id != cinema.Id)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(cinema);
    }

    try
    {
        await _service.UpdateAsync(id, cinema);
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