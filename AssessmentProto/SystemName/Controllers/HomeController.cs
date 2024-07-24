
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemName.Models;
using System.Diagnostics;
using SystemName.Models.ViewModels;

namespace SystemName.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext Context { get; set; }

        public HomeController(DatabaseContext ctx)
        {
            Context = ctx;
            Context.ChangeTracker.LazyLoadingEnabled = false;  //Increasing loading speed https://stackoverflow.com/questions/34724196/entity-framework-code-is-slow-when-using-include-many-times
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public IActionResult Index()
        {
            var products = Context.Product.Include(x => x.GenreObj).OrderBy(g => g.Name).ToList();
            //viewmodel to get subgenres and stocktake and price of products, takes stock, genres and products
            ProductViewModel viewModel = new ProductViewModel()
            {
                Products = Context.Product.Include(x => x.GenreObj).OrderBy(g => g.Name).ToList(),
                Stocktakes = Context.Stocktake.ToList(),
                Book_Genres = Context.Book_genre.ToList(),
                Game_Genres = Context.Game_genre.ToList(),
                Movie_Genres = Context.Movie_genre.ToList()
            };

            string isAdminValue = HttpContext.Session.GetString("IsAdmin"); 
            String Cart = HttpContext.Session.GetString("Cart"); //counts commas in string to get number of items
            if (Cart != null)
            {
                ViewBag.Cart = Cart.Count(x => x == ',');

            }
            else { 
                ViewBag.Cart = "0"; 
            }

            string test = HttpContext.Session.GetString("UserEmail");
            if (test != null) // test if there was a login
            {
                ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail"); 
                ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin"); // get role
            }
            else
            {
                // Handle the case where IsAdmin is not available or not a valid boolean value
                ViewBag.UserEmail = null;
                ViewBag.IsAdmin = "false";
            }


            return View(viewModel);
        }
        [HttpGet] //searchbar reload page where name contains search
        public IActionResult Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return RedirectToAction("Index"); //goes back to main if empty search box
            }
            else
            {
                // Perform the search based on the user's input
                var searchResults = Context.Product.Include(x => x.GenreObj).Where(p => p.Name.Contains(search)).OrderBy(g => g.Name).ToList();

                ProductViewModel viewModel = new ProductViewModel()
                {
                    Products = searchResults, // Set the search results to the Products property so a list of relevant products
                    Book_Genres = Context.Book_genre.ToList(),
                    Stocktakes = Context.Stocktake.ToList(),
                    Game_Genres = Context.Game_genre.ToList(),
                    Movie_Genres = Context.Movie_genre.ToList()
                };

                return View("Index", viewModel); // Display the search results in the Index view
            }
        }


    }
}