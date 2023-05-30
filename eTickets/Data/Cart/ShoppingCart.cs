using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using eTickets.Models;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ?ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ?ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession ?session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCart(context!) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems!
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Movie)
                .ToList());
        }

        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems!
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie!.Price * n.Amount)
                .Sum();
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems!
                .FirstOrDefault(n => n.Movie!.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId!,
                    Movie = movie,
                    Amount = 1
                    
                };
                _context.ShoppingCartItems!.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems!
                .SingleOrDefault(n => n.Movie!.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems!.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }
    }
}
