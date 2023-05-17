using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id{ get; set; }
        
        [Display(Name ="Logo")]
        public string Logo { get; set; }="";
        [Display(Name ="Name")]
       
        public string Name { get; set; }="";
        [Display(Name ="Description")]
     
        public string Description { get; set; }="";
        //Relations
        public List<Movie>?Movies { get; set; }

    }
}