using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    //[Route("about")]
    //[Route("[controller]")]                   // Token for controller (About). This is more robust than hard coding controller name
    //[Route("[controller]/[action]")]          // Use this instead of having [action] on every method. Makes the path specific for all the controller
    [Route("company/[controller]/[action]")]    // To add a literal path to the start
    public class AboutController
    {
        //[Route("")]                   // Leave blank for default
        public string Phone()
        {
            return "1+555+555+555";
        }

        //[Route("address")]            // Explicitly naming the method
        //[Route("[action]")]             // Use the name of the method
        public string Address()
        {
            return "USA";
        }
    }
}
