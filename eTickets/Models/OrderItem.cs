using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class OrderItem
    {
       [Key]
       public int Id {get;set;}
      public int Amount {get;set;}
      public double Price{get;set;}
      public string MovieId{get;set;}="";
      public Movie? Movie { get; set; }
      public int OrderId { get; set; }
public Order? Order { get; set; }


    }
}