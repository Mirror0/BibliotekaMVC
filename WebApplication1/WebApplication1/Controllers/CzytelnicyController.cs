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
    public class CzytelnicyController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Czytelnicy
        public ActionResult Index()
        {
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
            return View(czytelnik);
        }

        // GET: Czytelnicy/Create
        public ActionResult Create()
        {
            return View();
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
