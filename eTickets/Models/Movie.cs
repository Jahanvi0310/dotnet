using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class Movie:IEntityBase
    {
        [Key]
        public int Id { get; set; }
       [Display(Name ="Name")]
        public string Name { get; set; } = "";
        [Display(Name ="Description ")]
        public string Description { get; set; } = "";
        [Display(Name ="Price")]
        public double Price { get; set; } = 0.0;
        [Display(Name ="ImageUrl")]
        public string ImageUrl { get; set; } = "";
        [Display(Name ="StartDate")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [Display(Name ="EndDate")]
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
