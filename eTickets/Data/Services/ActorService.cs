using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Actor actor)
        {
            await _context.Actors!.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

       public async Task DeleteAsync(int id)
{
    var actor = await _context.Actors!.FindAsync(id);
    if (actor != null)
    {
        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }
}


       

       public async Task UpdateAsync(int id, Actor newActor)
        {
            var actor = await _context.Actors!.FindAsync(id);
            if (actor != null)
            {
                actor.FullName = newActor.FullName;
                actor.ProfilePictureUrl = newActor.ProfilePictureUrl;
                actor.Bio = newActor.Bio;

                await _context.SaveChangesAsync();
            }
        }
    }
}
