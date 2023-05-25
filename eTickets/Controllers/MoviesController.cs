using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var existingMovie = await _service.GetMovieByIdAsync(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            var newMovie = new NewMovieVM
            {
                Id = existingMovie.Id,
                Name = existingMovie.Name,
                Description = existingMovie.Description,
                Price = existingMovie.Price,
                ImageUrl = existingMovie.ImageUrl,
                StartDate = existingMovie.StartDate,
                EndDate = existingMovie.EndDate,
                MovieCategory = existingMovie.MovieCategory,
                CinemaId = existingMovie.CinemaId,
                ProducerId = existingMovie.ProducerId,
                ActorIds = existingMovie.Actors_Movies?.Select(am => am.ActorId).ToList()
            };

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(newMovie);
        }

       [HttpPost]
public async Task<IActionResult> Edit(NewMovieVM updatedMovie)
{
    if (ModelState.IsValid)
    {
        var existingMovie = await _service.GetMovieByIdAsync(updatedMovie.Id);

        if (existingMovie == null)
        {
            return NotFound();
        }

        var movieToUpdate = ConvertToMovie(updatedMovie, existingMovie);

        var result = await _service.UpdateMovieAsync(movieToUpdate);

        if (result != null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Failed to update the movie. Please try again.");
        }
    }

    var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
    ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
    ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
    ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

    return View(updatedMovie);
}

private Movie ConvertToMovie(NewMovieVM updatedMovie, Movie existingMovie)
{
    return new Movie
    {
        Id = updatedMovie.Id,
        Name = updatedMovie.Name,
        Description = updatedMovie.Description,
        Price = updatedMovie.Price,
        ImageUrl = updatedMovie.ImageUrl,
        StartDate = updatedMovie.StartDate,
        EndDate = updatedMovie.EndDate,
        MovieCategory = updatedMovie.MovieCategory,
        CinemaId = updatedMovie.CinemaId,
        ProducerId = updatedMovie.ProducerId,
        Actors_Movies = existingMovie.Actors_Movies // Assuming Actors_Movies is a property in the Movie model
    };
}
public async Task<IActionResult> Filter(string searchString)
{
    var allMovies=await _service.GetAllAsync(n=>n.Cinema!);
    if(!string.IsNullOrEmpty(searchString))
    {
        var filteredResult=allMovies.Where(n=>n.Name.Contains(searchString)||n.Description.Contains(searchString)).ToList();
        return View("Index",filteredResult);
    }
    return View("Index",allMovies);
}
    }
}
