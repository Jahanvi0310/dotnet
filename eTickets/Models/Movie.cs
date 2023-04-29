using System;
using System.ComponentModel.DataAnnotations;
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
    }
}
