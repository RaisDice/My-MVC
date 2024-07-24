using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemName.Models;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Drawing;
using SystemName.Models.ViewModels; //declarations

namespace SystemName.Controllers
{

    public class AccountController : Controller // vreate class
    {
        private DatabaseContext context { get; set; }

        public AccountController(DatabaseContext ctx) // get context, so gets database
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Login()  // when called goes to view action 
        {
            ViewBag.Action = "Login";  // Is just the Title


            return View("Login");
        }

        
        [HttpGet]
        public IActionResult List()  //Displays/gets list and IsAdmin and create view model the displays view
        {
            ViewBag.Action = "Account List"; 
            string test = HttpContext.Session.GetString("IsAdmin"); //gets IsAdmin from Session
            if (test != null) //if does exist create viewbag with IsAdmin value
            {

                ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            }
            else //else statement admin is false
            {
                ViewBag.IsAdmin = "False";
            }
            var TO = context.TO.OrderBy(x => x.customerID).ToList(); //TO is customer db grabs all items and sorts by id
            if (TO == null) //a check in case there is nothing in SQL table
            {
                return View("List", new List<TO>());
            }
            AccountViewModel viewModel = new AccountViewModel() // Create viewmodel 
            {
                Customer = TO,  //pass relvant TO values and User model
                Users = context.User.ToList()

            };
            
            return View("List", viewModel); // return view and viewmodel
        }
        [HttpGet]
        public IActionResult Edit(int customerID) //gets id of account to find details for relevant account
        {
            ViewBag.Action = "Edit"; 
            
            var customer = context.TO.Find(customerID); //find item relvant to ID in database 

            return View(customer); //return that row/item

        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            ViewBag.Action = "Login";
            if (ModelState.IsValid)  //checks if password is filled since this cant be checked in model
            {
                var password = Request.Form["Password"]; // get password from form 
                if (string.IsNullOrWhiteSpace(password)) //check if empty
                {
                    ModelState.AddModelError("Password", "Password is required."); //respond error
                    return View("Login", user);
                }
               
                var LogUser = context.User.FirstOrDefault(x => x.UserName == user.UserName);  //gets relavant user with inputed username
                if (LogUser != null)
                {
                                                // Gets SHA-256, Had Connors help here
                    string salt = LogUser.Salt; //gets salt of that user we got
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        
                        byte[] saltedPassword = Encoding.UTF8.GetBytes(password + LogUser.Salt); // adds salt from the users database


                        byte[] hashBytes = sha256.ComputeHash(saltedPassword);  // hashes salted password using sha256 as said in database dictionary
                        string hashedPassword = Convert.ToBase64String(hashBytes); //converts back to string from bytes, bytes are for security reasons

                       
                        if (hashedPassword == LogUser.HashPW)   // Compare hashed password with stored hashPW
                        {
                            HttpContext.Session.SetString("UserEmail", LogUser.Email); //not sure if used but is left here
                            HttpContext.Session.SetString("Logged", "True");
                            HttpContext.Session.SetString("IsAdmin", LogUser.isAdmin.ToString());   //didnt really understnad Identity and authorization, 
                            return RedirectToAction("Index", "Home");                               //so i just add their IsAdmin and logged to session then check for Admin or logged in on views
                        }
                    }
                }

               
                ModelState.AddModelError("Password", "Invalid username or password.");  // Passwords do not match
                return View("Login", user);
                
            }

            return View("Login");
        }


        [HttpGet]
        public IActionResult Register()
        {
            string test = HttpContext.Session.GetString("IsAdmin");
            if (test != null)
            {
                
                ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            } else
            {
                ViewBag.IsAdmin = "False";
            }
                ViewBag.Action = "Register";


            return View("Register");
        }

        [HttpPost] //https://stackoverflow.com/questions/4181198/how-to-hash-a-password where a majory of my hash code came from
        public IActionResult Register(User user) //this one adds new user
        {
            if (ModelState.IsValid) //check if valid and for empty space
            {
                if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
                {
                    ModelState.AddModelError("Password", "Password, Name, and Email are required.");
                    return View("Register", user);
                }
                // GEts SHA-256
                using (SHA256 sha256 = SHA256.Create())
                {
                    //get random salt
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] salt = new byte[32];//make salt
                    rng.GetBytes(salt);
                    string saltString = Convert.ToBase64String(salt).Substring(0, 32);//make salt a string


                    byte[] bytes = Encoding.UTF8.GetBytes(user.Password + saltString); //turn to bytes add salt to password
                    SHA256Managed sHA256ManagedString = new SHA256Managed();
                    byte[] hash = sHA256ManagedString.ComputeHash(bytes); //hash using sha256
                    

                    // Compute hash
                    
                    user.HashPW = Convert.ToBase64String(hash);
                    user.Salt = saltString;
                }

                // Add the user to your database (using Entity Framework or your data access layer)
                context.User.Add(user);
                context.SaveChanges();  // Save changes to the database

                return RedirectToAction("Index", "Home");
            }

            return View("Register", user);
        }

        

    }
}
