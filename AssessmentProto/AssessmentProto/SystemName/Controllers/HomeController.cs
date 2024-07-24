
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemName.Models;
using System.Diagnostics;

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
            var products = Context.Product.Include(x => x.GenreObj).OrderBy(g => g.Name);
            var productsList = products.ToList(); //get genre for each product

            return View(productsList);
        }
    }
}