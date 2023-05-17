using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id{ get; set; }
        [Display(Name ="Profile Picture")]
        public string ProfilePictureUrl { get; set; }="";
        [Required]
 [Display(Name ="FullName")]
        public string FullName { get; set; }="";
        [Display(Name ="Biography")]
        public string Bio { get; set; }="";
        //Relationships between producer and movie
        //one to many relation
       public List<Movie>? Movies { get; set; }

   

    }
}