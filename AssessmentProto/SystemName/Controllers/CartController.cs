using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SystemName.Models;
using SystemName.Models.ViewModels;

namespace SystemName.Controllers
{
    public class CartController : Controller //controller for ShoppingCart and payment
    {
        // Add an item to the cart
        private DatabaseContext context { get; set; }

        public CartController(DatabaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult AddToCart(int id) // stores/gets a atring of each item id then splits into array by comma
        {
            string cartString = HttpContext.Session.GetString("Cart"); //get current items
            if (cartString == null) //check empty
            {
                cartString = "";
            }
            List<string> cart = cartString.Split(',').ToList(); //split to array
            cart.Add(id.ToString()); //add new id to the arry
            cartString = string.Join(",", cart); //make array a string again joined by ,
            HttpContext.Session.SetString("Cart", cartString); //update cart

            return RedirectToAction("Index", "Home"); // Redirect to the home page

        }


        public IActionResult ViewCart()
        {

            string cartString = HttpContext.Session.GetString("Cart"); //gets cart string/array
            if (cartString == null) //check null
            {
                cartString = "";
            }
            List<string> cart = cartString.Split(',').ToList();
            List<Product> products = new List<Product>(); 


            foreach (var ProdId in cart)
            {
                
                if (int.TryParse(ProdId, out int id))  /// https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-7.0 
                { //used to stop error in line below. checks to see if it can be converted to int. 
                    Product product = context.Product.Include(p => p.GenreObj).Where(x => x.ID == id).FirstOrDefault();

                    if (product != null)
                    {
                        products.Add(product);


                    }
                }
            }
            ProductViewModel viewModel = new ProductViewModel() // make view model
            {
                Products = products, //pass products and stock take
                Stocktakes = context.Stocktake.ToList() 
            };

            return View("Cart",viewModel); // Pass the cart data to the view.
        }

        public IActionResult Payment(double Price) // Gets logged value and keeps Accumulated price from cart
        {

            ViewBag.Price = Price; //send pricce to payment
            string test = HttpContext.Session.GetString("Logged"); //check for logged
            if (test != null)
            {

                ViewBag.Logged = HttpContext.Session.GetString("Logged");
            }
            else
            {
                ViewBag.Logged = "False";
            }
            return View("Payment"); // Redirect to the home page or wherever you want.

        }
        public IActionResult Pay() //sends user home
        {
            
            
            return RedirectToAction("Index", "Home");// Redirect to the home page or wherever you want.

        }

    }
}   
