using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace eTickets.Models
{
    public class Actor
    {
        [Key]
        public int Id{ get; set; }
        [Display(Name ="Profile Picture ")]
        public string ProfilePictureUrl { get; set; }="";
        [Display(Name ="FullName")]
        public string FullName { get; set; }="";
         [Display(Name ="Biography")]
        public string Bio { get; set; }="";
        
        //Relationships
        public  List<Actor_Movie>? Actors_Movies { get; set; }

    }
}