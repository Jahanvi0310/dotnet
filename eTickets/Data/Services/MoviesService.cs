using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Models;
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

    }    
}
