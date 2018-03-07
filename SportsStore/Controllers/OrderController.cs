using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        //member vars
        private IOrderRepository mOrderRepository;
        private Cart mCart;

        //constructor
        public OrderController(IOrderRepository iOrderRepo, Cart cartService)
        {
            mOrderRepository = iOrderRepo;
            mCart = cartService;
        }

        public ViewResult List() => View(mOrderRepository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = mOrderRepository.Orders.FirstOrDefault(o => o.OrderID == orderID);

            if (order != null)
            {
                order.Shipped = true;
                mOrderRepository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (mCart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, cart is empty");
            }

            if (ModelState.IsValid)
            {
                order.Lines = mCart.Lines.ToArray();
                mOrderRepository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            mCart.Clear();
            return View();
        }
    }
}
