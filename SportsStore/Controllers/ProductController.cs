﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        //member var
        private IProductRepository mRepo;

        //properties
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            mRepo = repo;
        }


        public ViewResult List(string category,int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = mRepo.Products
                        .Where(p => p.Category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                        mRepo.Products.Count() :
                        mRepo.Products.Count(p => p.Category == category)
                },
                CurrentCategory = category

            });
        }


        public ViewResult Index()
        {
            return View();
        }

    }
}
