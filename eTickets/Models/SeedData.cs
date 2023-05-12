using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eTickets.Data;
using System;
using System.Linq;

namespace eTickets.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AppDbContext>>()))
        {
            // Look for any movies.
            if (context.Cinemas != null && !context.Cinemas.Any())
            {

                context.Cinemas.AddRange(new List<Cinema>()
                    {
new Cinema()
{
    Name="Cinema1",
    Logo="https://png.pngtree.com/png-clipart/20200826/original/pngtree-movie-logo-movie-letter-v-png-image_5469427.jpg",
    Description="This is the description of first cinema1"

},
new Cinema()
{
    Name="Cinema2",
    Logo="https://previews.123rf.com/images/dyahayufebriarini/dyahayufebriarini2005/dyahayufebriarini200500553/148633711-cinema-logo-movie-emblem-template-movie-production-logo-film-camera-logo-template-film-strip.jpg",
    Description="This is the description of first cinema2"

},
new Cinema()
{
    Name="Cinema3",
    Logo="https://img.freepik.com/premium-vector/cinema-logo-design-template_92405-24.jpg?w=2000",
    Description="This is the description of first cinema3"

},
new Cinema()
{
    Name="Cinema4",
    Logo="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQEN_G1IrklpujcTrr9cVPHsL9qlpGMJLGBNw&usqp=CAU",
    Description="This is the description of first cinema4"

},
new Cinema()
{
    Name="Cinema5",
    Logo="https://c8.alamy.com/comp/2DAXHR7/house-and-negative-film-for-movie-production-logo-design-2DAXHR7.jpg",
    Description="This is the description of first cinema5"

},
new Cinema()
{
    Name="Cinema6",
    Logo="https://img.freepik.com/premium-vector/cinema-camera-roll-film-logo-design-template_527727-210.jpgnam",
    Description="This is the description of first cinema6"

}
                    }
                        );
                context.SaveChanges();
                Console.WriteLine("Cinema added succefully");
            }

            //Actors
            if (context.Actors != null && !context.Actors.Any())
            {
                context.Actors.AddRange(new List<Actor>()
{
    new Actor(){
        FullName="Actor 1",
        Bio="This is bio of first actor",
       ProfilePictureUrl="https://benmarcum.com/wp-content/uploads/2017/03/Ben-Marcum-Photography_Headshot-Photographer_Louisville_Kentucky_Actor-Headshots_John-Wells_Jan-11-2017_025-1-1024x819.jpg",
    },
    new Actor(){
        FullName="Actor 2",
        Bio="This is bio of Second  actor",
       ProfilePictureUrl="https://images5.fanpop.com/image/photos/27000000/Hugh-Jackman-hugh-jackman-27058602-375-500.jpg",
    },
    new Actor(){
        FullName="Actor 3",
        Bio="This is bio of Third  actor",
       ProfilePictureUrl="https://www.iwmbuzz.com/wp-content/uploads/2021/02/top-5-hot-side-profiles-of-handsome-actors-from-brad-pitt-to-chris-evans-4.jpeg",
    },
    new Actor(){
        FullName="Actor 4",
        Bio="This is bio of Fourth  actor",
       ProfilePictureUrl="https://www.iwmbuzz.com/wp-content/uploads/2021/02/top-5-hot-side-profiles-of-handsome-actors-from-brad-pitt-to-chris-evans-5-578x920.jpeg",
    },
    new Actor(){
        FullName="Actor 5",
        Bio="This is bio of Fifth  actor",
       ProfilePictureUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEw1y5z0uli2I6JGMpLUpxIFSk2am5wFOHCRk7OqgkZWD5n474_KRyyRhAbiZNIzjzlAQ&usqp=CAU",
    },



});
                context.SaveChanges();
                Console.WriteLine("Actors added succefully");
            }

            //Producers
            if (context.Producers != null && !context.Producers.Any())
            {
                context.Producers.AddRange(new List<Producer>()
{
    new Producer()
    {
        FullName="Producer 1",
        Bio="Bio of Producer 1",
        ProfilePictureUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQym9JeI5rBExi-GfI5sYgSGF3vKwUNP8JgJQ&usqp=CAU"
    },
     new Producer()
     {
        FullName="Producer 2",
        Bio="Bio of Producer 2",
        ProfilePictureUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSqb-ykfiUh2A1mriMuNGhTJ6i_UXdwG7PFY5YmoJtSNeFv7UKkC6X6biU9CjHLFt-6ftg&usqp=CAU"
    },
     new Producer()
     {
        FullName="Producer 3",
        Bio="Bio of Producer 3",
        ProfilePictureUrl="https://ichef.bbci.co.uk/news/640/cpsprodpb/11C63/production/_117330827_drfrabzpicture.jpg"
    },
     new Producer()
     {
        FullName="Producer 4",
        Bio="Bio of Producer 4",
        ProfilePictureUrl="https://www.contentgarden.in/PICTURES/200619/WATERMARK/20171115-DLI_AKS_CT-%20EKTA%20KAPOOR%20%20(1).jpg"
    },
     new Producer()
     {
        FullName="Producer 5",
        Bio="Bio of Producer 5",
        ProfilePictureUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSQJlSGIOLyMS_OuvBS4-0iM3hgxKW1KS1a5CCO3arQDayk7L7HqZu9_kKmAOk6iylzgRA&usqp=CAU"
    }

});
                context.SaveChanges();
                Console.WriteLine("Producers added succefully");
            }
            //Movies
            if (context.Movies != null && !context.Movies.Any())
            {
                context.Movies.AddRange(new List<Movie>(){
new Movie(){
    Name="Ghost",
    Description="This is Ghost Movie Description",
    Price=78,
    ImageUrl="https://m.media-amazon.com/images/M/MV5BNDNhMGUwMzMtYTNkMi00ZjUxLWIwYzQtNzA2MjkzNmJkNzQxXkEyXkFqcGdeQXVyODE5NzE3OTE@._V1_FMjpg_UX1000_.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=1,
    ProducerId=1,
    MovieCategory=MovieCategory.Horror

},
new Movie(){
    Name="The Rescue",
    Description="This is Action Movie Description",
    Price=38,
    ImageUrl="https://m.media-amazon.com/images/M/MV5BODIwNGZiNjQtM2FhYy00NDZkLTlmOTUtZjMzNTE2YWI2NzRlXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=2,
    ProducerId=2,
    MovieCategory=MovieCategory.Documentary

},
new Movie(){
    Name="We are Millers",
    Description="This is Comedy Movie Description",
    Price=89,
    ImageUrl="https://filmfare.wwmindia.com/content/2021/aug/best-comedy-movies-hollywood-were-the-millers.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=4,
    ProducerId=4,
    MovieCategory=MovieCategory.Comedy

},
new Movie(){
    Name="Kinder",
    Description="This is Cartoon Movie Description",
    Price=68,
    ImageUrl="https://i.pinimg.com/236x/a7/33/ac/a733ac60bd812ac714e58fe16726a31a.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=5,
    ProducerId=3,
    MovieCategory=MovieCategory.Cartoon

},
new Movie(){
    Name="Korean Drama",
    Description="This is Drama Movie Description",
    Price=82,
    ImageUrl="https://publish.purewow.net/wp-content/uploads/sites/2/2020/11/best-korean-dramas-always.jpg?fit=728%2C524",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=2,
    ProducerId=4,
    MovieCategory=MovieCategory.Drama

},
new Movie(){
    Name="War",
    Description="This is Action Movie Description",
    Price=68,
    ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTvl5xPgxQYCSotyJuAt8YT1annzgDJtRhYmQ&usqp=CAU",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=3,
    ProducerId=3,
    MovieCategory=MovieCategory.Action

},
new Movie(){
    Name="Hangover",
    Description="This is Comedy Movie Description",
    Price=28,
    ImageUrl="https://www.scrolldroll.com/wp-content/uploads/2022/04/The-Hangover-Best-Hindi-Dubbed-Comedy-Movies-scaled.jpeg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=1,
    ProducerId=3,
    MovieCategory=MovieCategory.Comedy

},
new Movie(){
    Name="March of penguins",
    Description="This is Documentary Movie Description",
    Price=18,
    ImageUrl="https://m.media-amazon.com/images/M/MV5BMTM1NDc5MDYxMl5BMl5BanBnXkFtZTcwMjMzNDAzMQ@@._V1_.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=1,
    ProducerId=3,
    MovieCategory=MovieCategory.Documentary

},
new Movie(){
    Name="Home Scary",
    Description="This is Horror Movie Description",
    Price=68,
    ImageUrl="https://m.media-amazon.com/images/M/MV5BMjM4MjQ4MTMwNV5BMl5BanBnXkFtZTgwOTk2MjQyODE@._V1_FMjpg_UX1000_.jpg",
    StartDate=DateTime.Now,
    EndDate=DateTime.Now.AddDays(7),
    CinemaId=1,
    ProducerId=3,
    MovieCategory=MovieCategory.Horror

}

                    });
                context.SaveChanges();
                Console.WriteLine("Movies added successfuly");
            }
            //relation between actors nd movies
            if (context.Actor_Movies != null && !context.Actor_Movies.Any())
{
    var actorMovies = new List<Actor_Movie>{
        new Actor_Movie(){ ActorId = 1, MovieId = 2 },
        new Actor_Movie(){ ActorId = 4, MovieId = 3 },
        new Actor_Movie(){ ActorId = 1, MovieId = 3 },
        new Actor_Movie(){ ActorId = 1, MovieId = 4 },
        new Actor_Movie(){ ActorId = 2, MovieId = 5 },
        new Actor_Movie(){ ActorId = 1, MovieId = 3 },
        new Actor_Movie(){ ActorId = 4, MovieId = 2 },
        new Actor_Movie(){ ActorId = 5, MovieId = 2 },
        new Actor_Movie(){ ActorId = 3, MovieId = 5 },
        new Actor_Movie(){ ActorId = 4, MovieId = 4 },
        new Actor_Movie(){ ActorId = 4, MovieId = 5 }
    };

    foreach (var am in actorMovies)
    {
        var existing = context.Actor_Movies.Find(am.ActorId, am.MovieId);
        if (existing != null)
        {
            // Update existing entity
            existing.ActorId = am.ActorId;
        }
        else
        {
            // Add new entity
            context.Actor_Movies.Add(am);
        }
    }

    // Save changes to add Actor_Movie relationships to the database
    context.SaveChanges();

                Console.WriteLine("Actor_movies added succefully");
            }
        }
    }
}

