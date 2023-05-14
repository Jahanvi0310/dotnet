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
        [Required(ErrorMessage ="Profile Picture Required")]
        public string ProfilePictureUrl { get; set; }="";
        [Display(Name ="FullName")]
        [Required(ErrorMessage ="Full Name Required")]
        public string FullName { get; set; }="";
         [Display(Name ="Biography")]
         [Required(ErrorMessage ="Biography Required")]
        public string Bio { get; set; }="";
        
        //Relationships
        public  List<Actor_Movie>? Actors_Movies { get; set; }

    }
}