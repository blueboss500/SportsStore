using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        //member vars
        private IProductRepository mRepo;

        //constru
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            mRepo = repo;
        }


        public IViewComponentResult Invoke()
        {
            //should really use a ViewModel
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(mRepo.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
