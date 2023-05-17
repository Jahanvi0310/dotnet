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
    public class ProducersController:Controller
    {
  private readonly IProducersService _service;
public ProducersController(IProducersService service)
{
    _service=service;
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
public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")] Producer producer)
{
    if (!ModelState.IsValid)
    {
        return View(producer);
    }

     await _service.AddAsync(producer);
     

    return RedirectToAction(nameof(Index));
}
public async Task<IActionResult> Details(int id)
{
    var producer = await _service.GetByIdAsync(id);

    if (producer == null)
    {
        return RedirectToAction("NotFound", "Producers");
    }

    return View(producer);
}
public async Task<IActionResult> Delete(int id)
{
    var producer = await _service.GetByIdAsync(id);

    if (producer == null)
    {
        return NotFound();
    }

    return View(producer);
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
public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureUrl, Bio")] Producer producer)
{
    if (id != producer.Id)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(producer);
    }

    try
    {
        await _service.UpdateAsync(id, producer);
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