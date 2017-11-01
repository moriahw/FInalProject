using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult Member()
        {
            maedbEntities1 Members = new maedbEntities1();

            List<Event> UserEvent = Members.Events.ToList();
            List<Food> UserFood = Members.Foods.ToList();


            // random number generator 
            Random rnd = new Random(Guid.NewGuid().GetHashCode()); //generates a new sequence each time the method is called
            int Number1 = rnd.Next(1, UserEvent.Count);

            //Random rnd2 = new Random(Guid.NewGuid().GetHashCode());
            int Number2 = rnd.Next(1, UserFood.Count);

            ViewBag.EventData= UserEvent[Number1];
            /////////////////
            ViewBag.FoodData = UserFood[Number2];

            return View();
        }

        public ActionResult Random()
        {
            return View();
        }
    }
}