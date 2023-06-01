using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class OrderItem
    {
       [Key]
       public int Id {get;set;}
      public int Amount {get;set;}
      public double Price{get;set;}
      public int MovieId{get;set;}
      [ForeignKey("MovieId")]
      public Movie? Movie { get; set; }
      public int OrderId { get; set; }
      [ForeignKey("OrderId")]
public Order? Order { get; set; }

       
    }
}