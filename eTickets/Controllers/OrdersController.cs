using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using eTickets.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Cart;
using eTickets.Data.ViewModels;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart,IOrderService orderService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
      public async Task<RedirectToActionResult> AddToShoppingCart(int id)
{
    var item = await _moviesService.GetMovieByIdAsync(id);
    if (item != null)
    {
        _shoppingCart.AddItemToCart(item);
        var cartItemCount = _shoppingCart.GetShoppingCartItems().Sum(n => n.Amount);
        HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
    }
    return RedirectToAction("Index", "Orders");
}

       public async Task<RedirectToActionResult> RemoveFromShoppingCart(int id)
{
    var item = await _moviesService.GetMovieByIdAsync(id);
    if (item != null)
    {
        _shoppingCart.RemoveFromCart(item);
    }
    return RedirectToAction("Index", "Orders");
}
public async Task<IActionResult> CompletedOrder()
{
    var items=_shoppingCart.GetShoppingCartItems();
    string userId="";
    string userEmailAddress="";
    await _orderService.StoreOrderAsync(items,userId,userEmailAddress);
    await _shoppingCart.ClearShoppingCartAsync();
    return View("OrderCompleted");
}
public async Task <IActionResult> AllOrder()
{
    string userId="";
var orders=await _orderService.GetOrdersByUserIdAsync(userId);
return View(orders);
}
    }
}
