using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        //member vars
        private IProductRepository mRepo;
        private Cart mCart;


        public CartController(IProductRepository repo, Cart cartService)
        {
            mRepo = repo;
            mCart = cartService;
        }

        //helpers
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
            
        }


        //methods
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = mCart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            //find product in repo
            Product product = mRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                mCart.AddItem(product,1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }


        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            //find product in repo
            Product product = mRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                mCart.RemoveLine(product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }
    }
}