using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

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

            return View();
        }

        public ActionResult Product()
        {

            maedbEntities2 Members = new maedbEntities2();
            List<Souvenir> AllSouvenirs = Members.Souvenirs.ToList();
            ViewBag.EmpList = AllSouvenirs;



            return View("Contact");
        }

        [Authorize]
        public ActionResult Member()
        {
            //call the API using the URL. thia can be done by using HTTP requests and responses

            HttpWebRequest WR = WebRequest.CreateHttp("http://forecast.weather.gov/MapClick.php?lat=42.3331&lon=-83.0496&FcstType=json");

            WR.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();

            StreamReader reader = new StreamReader(Response.GetResponseStream());//get a reference to the response stream

            string WeatherData = reader.ReadToEnd();// reads the data and stores it in the string

            JObject JsonData = JObject.Parse(WeatherData);

            ViewBag.Weather = JsonData["data"]["weather"];

            ViewBag.Time = JsonData["time"]["startPeriodName"];

            ViewBag.Temp = JsonData["data"]["temperature"];

            ViewBag.Text = JsonData["data"]["text"];

            ///////////////////////////////////////////////////
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

        [Authorize]
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

        public ActionResult DeleteItem(string name)
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
        


            Souvenir Option = ShoppingBag.Find(x=>x.Souvenirname == name);
            ShoppingBag.Remove(Option);

            Session["Cart"] = ShoppingBag;// save changes you made to your cart! 

            
            ViewBag.Cart = ShoppingBag;

          

            return View("Cart") ;


        }

        public ActionResult Checkout()
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


            double stotal = 0;
            


            for (int i = 0; i < ShoppingBag.Count; i++)
            {
                stotal = stotal + double.Parse(ShoppingBag[i].Price);
              //  gtotal = stotal * Quantity;


            }


            ViewBag.gtotal = stotal;


           // Session["GrandTotal"] = gtotal;




            return View();
        }

        public ActionResult SavePaymentInfo()
        {

            return View("Confirm");
            
        }
    }
}