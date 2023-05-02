using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data;
namespace eTickets.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
       
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; } = 0.0;
        public string ImageUrl { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
    public MovieCategory MovieCategory {get;set;}

    //Relationship 
    //many to many relationship
    public List<Actor_Movie>? Actors_Movies { get; set; }

//Cinema
    //on to many relationship bewteen cinema and movie.to define the cenima join there should be foreign key so as in actor_movie
//To define foregn key
public int CinemaId { get; set; }
[ForeignKey("CinemaId")]
    public Cinema ?Cinema { get; set; }


    //Producer
    public int ProducerId { get; set; }
    [ForeignKey("ProducerId")]
    public Producer?Producer{ get; set; }
    }
}
