using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class NewMovieVM
    {
        [Key]
      public int Id {get;set;}
      
       [Display(Name ="Name")]
       [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } = "";
        [Display(Name ="Description ")]
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; } = "";
        [Display(Name ="Price")]
        [Required(ErrorMessage ="Price is required")]
        public double Price { get; set; } = 0.0;
        [Display(Name ="ImageUrl")]
        [Required(ErrorMessage ="ImageUrl is required")]
        public string ImageUrl { get; set; } = "";
        [Display(Name ="StartDate")]
        [Required(ErrorMessage ="StartDate is required")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [Display(Name ="EndDate")]
        [Required(ErrorMessage ="EndDate is required")]
        public DateTime EndDate { get; set; } = DateTime.MinValue;
 [Display(Name ="Select Category")]
        [Required(ErrorMessage =" Category is required")]
    public MovieCategory MovieCategory {get;set;}

    //Relationship 
    //many to many relationship
     [Display(Name ="Select actor(s)")]
        [Required(ErrorMessage =" actor is required")]
    public List<int>? ActorIds { get; set; }

//Cinema
    //on to many relationship bewteen cinema and movie.to define the cenima join there should be foreign key so as in actor_movie
//To define foregn key
[Display(Name ="Select Cinema ")]
        [Required(ErrorMessage =" Cinema is required")]
public int CinemaId { get; set; }



    //Producer
    [Display(Name ="Select Producer")]
        [Required(ErrorMessage =" Producer is required")]
    public int ProducerId { get; set; }
    
    }
}
