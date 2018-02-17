using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        #region Course Progression
        //public string Index()
        //{
        //    return "Hello from the HomeController!";
        //}

        //public IActionResult IndexDemo()
        //{
        //    // Try to avoid using httpcontext in a controller. Other methods are available to access response or request.
        //    //this.HttpContext.Response.Headers

        //    // Send a BadRequest error back
        //    //return this.BadRequest();

        //    // Return a file
        //    //this.File();
            
        //    return Content("Hello from the HomeController!");
        //}

        //public IActionResult IndexSingleItem()
        //{
        //    var model = new Restaurant {Id = 1, Name = "Scott's Pizza Place"};

        //    // This will return the model to be encoded into JSON/XMl/whatever
        //    //return new ObjectResult(model);

        //    // This will return a view result. With no name specified, it will default to method name (Index)

        //    // /Views/Home/Home.cshtml          - If you have a view only used by a single controller
        //    // /Views/Shared/Home.cshtml        - If the view is to be used by multiple controllers

        //    return View(model);
        //}

#endregion

        public IActionResult Index()
        {
            // Simple return of all the restaurants
            //var model = _restaurantData.GetAll();

            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            // Just echo back the id. This is coming from the routing information in Startup.cs
            //             routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            //return Content(id.ToString());

            var model = _restaurantData.Get(id);
            if (model == null)
            {
                //return View("NotFound");                  // Return a specific view with a nice message
                //return NotFound();                        // Will return a 404 error. Not nice, really.
                //return RedirectToAction("Index");         // Go to an index action on this controller
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Route constraint to tell framework to use this action for a GET request
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // This is VERY important for web apps using POSTs
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            // ModelState allows you to interrogate what happens with the model binding process
            if (ModelState.IsValid)
            {

                var newRestaurant = new Restaurant
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine
                };

                newRestaurant = _restaurantData.Add(newRestaurant);

                // Don't return a view as this could return another POST screen. If the user refreshes, it could POST again...BAD!
                //return View("Details", newRestaurant);

                // Instead, redirect using a GET request
                return RedirectToAction(nameof(Details), new {id = newRestaurant.Id});
            }
            else
            {
                // User submitted data that didn't pass validation rules
                return View();
            }
        }
    }
}
