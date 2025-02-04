using System.Linq;
using System.Web.Mvc;
using Kutse_App.Models;

namespace Kutse_App.Controllers
{
    public class PühadController : Controller
    {
        private GuestContext db = new GuestContext();
        public ActionResult Pühad()
        {
            return View(db.Pühad.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Pühad pühad)
        {
            db.Pühad.Add(pühad);
            db.SaveChanges();
            return RedirectToAction("Pühad");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Pühad p = db.Pühad.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Pühad pühad)
        {
            db.Entry(pühad).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Pühad");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Pühad p = db.Pühad.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pühad p = db.Pühad.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            db.Pühad.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Pühad");
        }
    }
}