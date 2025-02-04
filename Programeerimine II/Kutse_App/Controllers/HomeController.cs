using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            
            string picturePath;

            if (hour < 10)
            {
                picturePath = "https://upload.wikimedia.org/wikipedia/commons/7/7d/Morning%2C_just_after_sunrise%2C_Namibia.jpg";
            }
            else if (hour < 17)
            {
                picturePath = "https://upload.wikimedia.org/wikipedia/commons/2/2e/Brisbane_Water_National_Park_sunrise.jpg";
            }
            else if (hour < 21)
            {
                picturePath = "https://upload.wikimedia.org/wikipedia/commons/3/3a/Evening_in_Parambikkulam%2C_Kerala%2C_India.jpg";
            }
            else
            {
                picturePath = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Savault_Chapel_Under_Milky_Way_BLS.jpg";
            }

            ViewBag.PicturePath = picturePath;

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
            Meeldetuletus(guest, "");
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult Meeldetuletus(Guest guest, string Meeldetuletus)
        {
            if (!string.IsNullOrEmpty(Meeldetuletus))
            {
                try
                {
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "othermodstactics@gmail.com";
                    WebMail.Password = "uzct ocxw uyte abyr";
                    WebMail.From = "othermodstactics@gmail.com";

                    WebMail.Send(guest.Email, "Meeldetuletus", guest.Nimi + ", ara unusta. Pidu toimub 25.01.25! Sind ootavad väga!",
                    null, guest.Email
                   );

                    ViewBag.Message = "Kutse saadetud!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Tekkis viga kutse saatmisel: " + ex.Message;
                }
            }

            return View("Thanks", guest);
        }

        GuestContext db = new GuestContext();
        [Authorize(Roles = "User")]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        
        [Authorize(Roles = "User")]
        public ActionResult Details(int id)
        {
            var guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }
        
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
    }
}