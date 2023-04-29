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
        public String ProfilePictureUrl { get; set; }="";
        public String FullName { get; set; }="";
        public String Bio { get; set; }="";

    }
}