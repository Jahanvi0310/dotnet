using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using eTickets.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
{
    // Retrieve orders based on the user's ID
    var orders = await _context.Orders!
        .Include(n => n.OrderItems!)
        .ThenInclude(n => n.Movie)
        .Where(n => n.UserId == userId)
        .ToListAsync();

    return orders!;
}
 public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
{
    // Create an order based on the shopping cart items
    var order = new Order()
    {
        UserId = userId,
        Email = userEmailAddress
    };

    await _context.Orders!.AddAsync(order);
    await _context.SaveChangesAsync();

    foreach (var item in items)
    {
        var orderItem = new OrderItem()
        {
            Amount = item.Amount,
            MovieId = item.Movie!.Id,
            OrderId = order.Id, // Assuming order.Id is an integer
            Price = item.Movie.Price
        };
        await _context.OrderItems!.AddAsync(orderItem);
    }
    await _context.SaveChangesAsync();
}


    }
}
