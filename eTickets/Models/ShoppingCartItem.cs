using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace eTickets.Models
{
    public class ShoppingCartItem
    {
 [Key]
    public int Id{ get; set; }
    public Movie? Movie{ get; set; }
    public int Amount {get;set;}
    //to clear the database after storing so we will do order and shoppingcart will delete
    public string ShoppingCartId{get;set;}="";
    }
   
}