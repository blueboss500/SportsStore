using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        //member vars
        private Cart mCart;

        //constr
        public CartSummaryViewComponent(Cart cartService)
        {
            mCart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(mCart);
        }
    }
}
