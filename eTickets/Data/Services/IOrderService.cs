using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using eTickets.Data.Base;
namespace eTickets.Data.Services
{
    public interface IOrderService
    {
Task StoreOrderAsync(List<ShoppingCartItem> items,string userId, string userEmailAddress);
Task <List<Order>> GetOrdersByUserIdAsync(string userId);

    }
}