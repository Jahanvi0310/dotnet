using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id{ get; set; }
        public string ProfilePictureUrl { get; set; }="";
        [Required]
        public string FullName { get; set; }="";
        public string Bio { get; set; }="";

    }
}