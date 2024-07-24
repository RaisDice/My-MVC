using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemName.Models;
using SystemName.Models.ViewModels;

namespace SystemName.Controllers
{
   
    public class ProductController : Controller
    {
        private DatabaseContext context { get; set; }

        public ProductController(DatabaseContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add"; 
            ViewBag.Genres = context.Genre.OrderBy(g => g.Name).ToList();
            
            return View("Edit", new Product()); //new product add and goes to view
        }
      
        [HttpGet]
        public IActionResult Edit(int id) //gets Product from id 
        {
            ViewBag.Action = "Edit"; //returns Product and sets viewbag for genres when editing
            ViewBag.Genres = context.Genre.OrderBy(g => g.Name).ToList();
            var product = context.Product.Find(id);
           
            return View(product);

        }

        public IActionResult View(int id) { //same as edit but will go to different view

            ViewBag.Genres = context.Genre.OrderBy(g => g.Name).ToList();
            var product = context.Product.Find(id);
            //ViewBag.product = product;
            return View(product);

        }

    [HttpPost]
        public IActionResult Edit(Product product) //when the user submits form
        {
            if (ModelState.IsValid)  //checks if new product or not and adds/updates
            {
                if  (product.ID == 0) { 
                    context.Product.Add(product);
                }
                else { 
                context.Product.Update(product);
                 }
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else //if their inputs werent valid reset.
            {
                ViewBag.Action = (product.ID == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genre.OrderBy(g => g.Name).ToList();
                
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Product.Find(id); //get prudoct id for delte
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)  //removes product from db
        {
            context.Product.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");//sends home
        }


    }
}