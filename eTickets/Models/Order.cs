using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;
namespace eTickets.Models
{
    public class Order
    {
       [Key]
       public int Id {get;set;}
       public string Email{get;set;}="";
       public string ?UserId{get;set;}
       public string? UserEmailAddress { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    }
}