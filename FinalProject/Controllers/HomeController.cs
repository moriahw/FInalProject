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

        public ActionResult Product()
        {

            maedbEntities2 Members = new maedbEntities2();
            List<Souvenir> AllSouvenirs = Members.Souvenirs.ToList();
            ViewBag.EmpList = AllSouvenirs;
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }
       
        public ActionResult Member()
        {
            maedbEntities2 Members = new maedbEntities2();

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

        public ActionResult Team()
        {
            return View();
        }

        public ActionResult AddtoCart(string Souvenir)
        {
            List<Souvenir> ShoppingBag; // reference to null 

            if (Session["Cart"] == null)// the cart is empty! 
            {

                Session.Add("Cart", new List<Souvenir>());

                ShoppingBag = new List<Souvenir>();
            }

            else// user has items in the cart, so go and retrive it!
            {
                ShoppingBag = (List<Souvenir>)Session["Cart"];

            }
            ///////////////////////////
            maedbEntities2 ItemList = new maedbEntities2();


            Souvenir Option = ItemList.Souvenirs.Find(Souvenir);
            ShoppingBag.Add(Option);

            Session["Cart"] = ShoppingBag;// save changes you made to your cart! 

            ViewBag.Cart = ShoppingBag;

            maedbEntities2 NewList = new maedbEntities2();
            List<Souvenir> AllProducts = NewList.Souvenirs.ToList();
            ViewBag.PList = AllProducts;

            return RedirectToAction("Product");
        }

        public ActionResult Cart()
        {


            List<Souvenir> ShoppingBag; // reference to null 

            if (Session["Cart"] == null)// the cart is empty! 
            {

                Session.Add("Cart", new List<Souvenir>());

                ShoppingBag = new List<Souvenir>();
            }

            else// user has items in the cart, so go and retrive it!
            {
                ShoppingBag = (List<Souvenir>)Session["Cart"];

            }


            ViewBag.Cart = ShoppingBag;
            return View();

        }
    }
}