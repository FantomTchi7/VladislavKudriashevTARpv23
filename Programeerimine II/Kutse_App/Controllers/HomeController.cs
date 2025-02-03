using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            int month = DateTime.Now.Month;
            
            switch (month)
            {
                case 1:
                    ViewBag.Holiday = "Hea uut aastat!";
                    break;
                default:
                    ViewBag.Holiday = "";
                    break;

            }
            ViewBag.Message = "Palun tule! Ootan sind minu poele!";
            ViewBag.PicturePath = hour < 10 ? "https://upload.wikimedia.org/wikipedia/commons/7/7d/Morning%2C_just_after_sunrise%2C_Namibia.jpg" : (hour < 17 ? "https://upload.wikimedia.org/wikipedia/commons/2/2e/Brisbane_Water_National_Park_sunrise.jpg" : (hour < 21 ? "https://upload.wikimedia.org/wikipedia/commons/3/3a/Evening_in_Parambikkulam%2C_Kerala%2C_India.jpg" : "https://upload.wikimedia.org/wikipedia/commons/6/6d/Savault_Chapel_Under_Milky_Way_BLS.jpg")); 
            ViewBag.Greeting = hour < 10 ? "Hommikust!" : (hour < 17 ? "Päevast!" : (hour < 21 ? "Õhtust!" : "Head ööd!"));
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            SaadaEmail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }
        public void SaadaEmail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "othermodstactics@gmail.com";
                WebMail.Password = "uzct ocxw uyte abyr";
                WebMail.From = "othermodstactics@gmail.com";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " Vastus " + ((guest.WillAttend ?? false) ?
                    "Tulen kindlasti!" : "Kahjuks ei saa tulla!"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception;
            }
        }
    }
}