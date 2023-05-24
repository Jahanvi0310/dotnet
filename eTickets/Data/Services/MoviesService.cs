using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Models;
using eTickets.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

using eTickets.Data.Base;

namespace eTickets.Data.Services
{
    public class MoviesService :EntityBaseRepository<Movie>,IMoviesService
    {
       
private readonly AppDbContext _context;
        public  MoviesService(AppDbContext context):base(context)
        
        {
_context=context;
        }
        public async Task<Movie> GetMovieByIdAsync(int Id)
        {
var details=await _context.Movies!
.Include(c=>c.Cinema!)
.Include(p=>p.Producer!)
.Include(am=>am.Actors_Movies!).ThenInclude(a=>a.Actor)
.FirstOrDefaultAsync(n => n.Id == Id);
return details!;
        
    }

public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
{
    var response = new NewMovieDropdownsVM();
    response.Actors = await _context.Actors!.OrderBy(n => n.FullName).ToListAsync();
response.Cinemas=await _context.Cinemas!.OrderBy(n=>n.Name).ToListAsync();
response.Producers=await _context.Producers!.OrderBy(n=>n.FullName).ToListAsync();
return response;

    }    
    public async Task<Movie> CreateMovieAsync(NewMovieVM newMovie)
{
    // Map the NewMovieVM object to a Movie object
    var movie = new Movie
    {
        Name = newMovie.Name,
        Description = newMovie.Description,
        Price = newMovie.Price,
        ImageUrl = newMovie.ImageUrl,
        StartDate = newMovie.StartDate,
        EndDate = newMovie.EndDate,
        MovieCategory = newMovie.MovieCategory,
        CinemaId = newMovie.CinemaId,
        ProducerId = newMovie.ProducerId
    };

    // Create a list of Actor_Movie objects to associate actors with the movie
    if (newMovie.ActorIds != null && newMovie.ActorIds.Any())
    {
        movie.Actors_Movies = newMovie.ActorIds.Select(actorId => new Actor_Movie
        {
            ActorId = actorId,
            MovieId = movie.Id
        }).ToList();
    }

    // Add the movie to the context
    _context.Movies!.Add(movie);

    // Save the changes to the database
    await _context.SaveChangesAsync();

    return movie;
}

       public async Task<Movie> UpdateMovieAsync(Movie updatedMovie)
        {
            // Retrieve the existing movie from the database
            var existingMovie = await _context.Movies!.FindAsync(updatedMovie.Id);

            if (existingMovie == null)
                return null!;

            // Update the movie properties with the new values
            existingMovie.Name = updatedMovie.Name;
            existingMovie.Description = updatedMovie.Description;
            existingMovie.Price = updatedMovie.Price;
            existingMovie.ImageUrl = updatedMovie.ImageUrl;
            existingMovie.StartDate = updatedMovie.StartDate;
            existingMovie.EndDate = updatedMovie.EndDate;
            existingMovie.MovieCategory = updatedMovie.MovieCategory;
            existingMovie.CinemaId = updatedMovie.CinemaId;
            existingMovie.ProducerId = updatedMovie.ProducerId;

            // Update the actors associated with the movie
            if (updatedMovie.Actors_Movies != null)
            {
                // Clear the existing actors associated with the movie
                existingMovie.Actors_Movies!.Clear();

                // Associate the updated actors with the movie
                foreach (var actorMovie in updatedMovie.Actors_Movies)
                {
                    existingMovie.Actors_Movies.Add(actorMovie);
                }
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return existingMovie;
        }
    }
}
