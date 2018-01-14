using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CzytelnicyController : Controller
    {
        private LibDBEntities db = new LibDBEntities();
        // GET: Czytelnicy
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            int id = 0;
            if (Session["UserID"] != null)
            {
                id = Int32.Parse(Session["UserID"].ToString());
            }
            //ViewBag.UserName = db.Czytelnik.Find(id).Uzytkownik.ToString();
            return View(db.Czytelnik.ToList());
        }

        // GET: Czytelnicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelnik.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRoleString = RolaToString(czytelnik.Rola);
            return View(czytelnik);
        }

        // GET: Czytelnicy/Create
        public ActionResult Create()
        {
            return View();
        }

        private string RolaToString(int? rola)
        {
            switch (rola)
            {
                case 0:
                    {
                        return "Czytelnik";
                    }
                case 1:
                    {
                        return "Pracownik";
                    }
                case 2:
                    {
                        return "Administator";
                    }
                default:
                    {
                        return "Nieznana Rola: " + rola;
                    }
            }
        }

        private SelectList PopulateDropDownList(Czytelnik czytelnik)
        {
            var roles = db.Czytelnik.GroupBy(x => x.Rola);
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item = null;
            foreach (var role in roles)
            {
                switch (role.Key)
                {
                    case 0:
                        {
                            item = new SelectListItem() { Text = "Czytelnik", Value = "0" };
                            items.Add(item);
                            break;
                        }
                    case 1:
                        {
                            items.Add(new SelectListItem() { Text = "Pracownik", Value = "1" });
                            break;
                        }
                    case 2:
                        {
                            items.Add(new SelectListItem() { Text = "Administator", Value = "2" });
                            break;
                        }
                    default:
                        {
                            items.Add(new SelectListItem() { Text = "Nieznana Rola: " + role.Key, Value = role.Key.ToString() });
                            break;
                        }
                }
                if (item.Value.Equals(czytelnik.Rola.ToString()))
                {
                    item.Selected = true;
                }
            }


            return new SelectList(items,"Value","Text",czytelnik.Rola);
        }

        // POST: Czytelnicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Uzytkownik,Haslo,Email")] Czytelnik czytelnik)
        {
            if (ModelState.IsValid)
            {
                db.Czytelnik.Add(czytelnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(czytelnik);
        }

        // GET: Czytelnicy/Edit/5
        public ActionResult Edit(int? id)
        {
            Czytelnik user = null;
            ViewBag.UserRole = null;
            if (Session["UserID"] != null)
            {
                user = db.Czytelnik.Find(Int32.Parse(Session["UserID"].ToString()));
            }
            if (user != null)
            {
                ViewBag.UserRole = user.Rola;
            }
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelnik.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRoleString = RolaToString(czytelnik.Rola);
            SelectList list = PopulateDropDownList(czytelnik);
            //list.Where(x => x.Value.Equals(czytelnik.Rola.ToString())).FirstOrDefault().Selected = true;
            ViewBag.RoleSelectList = list;
            return View(czytelnik);
        }

        // POST: Czytelnicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko,Uzytkownik,Haslo,Email")] Czytelnik czytelnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(czytelnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(czytelnik);
        }

        // GET: Czytelnicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czytelnik czytelnik = db.Czytelnik.Find(id);
            if (czytelnik == null)
            {
                return HttpNotFound();
            }
            return View(czytelnik);
        }

        // POST: Czytelnicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Czytelnik czytelnik = db.Czytelnik.Find(id);
            db.Czytelnik.Remove(czytelnik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
