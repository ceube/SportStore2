﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {

        private IProductRepository repository;
        //
        // GET: /Cart/

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new  { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId,string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

                    if(product!=null)
                    {
                        GetCart().RemoveLine(product);
                    }
            return RedirectToAction("Index", new{returnUrl});
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart==null)
            {
            cart=new Cart();
            Session["Cart"] = cart;
            }
            return cart;

        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });

        }



    }
}
