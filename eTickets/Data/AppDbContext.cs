using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });
            modelBuilder.Entity<Actor_Movie>().HasOne(am => am.Actor).WithMany(a => a.Actors_Movies).HasForeignKey(am => am.ActorId);
            modelBuilder.Entity<Actor_Movie>().HasOne(am => am.Movie).WithMany(m => m.Actors_Movies).HasForeignKey(am => am.MovieId);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Cinema> ?Cinemas {get;set;}
        public DbSet<Actor>? Actors { get; set; }
        public DbSet<Actor_Movie>?Actor_Movies { get; set; }
        public DbSet <Producer> ? Producers { get; set; }

        //order Related tabels
          public DbSet <Order> ? Orders { get; set; }
          public DbSet <OrderItem> ? OrderItems { get; set; }
public DbSet<ShoppingCartItem>?ShoppingCartItems{get;set;}
         
    }
}