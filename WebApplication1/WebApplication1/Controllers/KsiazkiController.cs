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
    public class KsiazkiController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Ksiazki
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var ksiazka = db.Ksiazka.Include(k => k.Autor).Include(k => k.Slowo_Kluczowe).Include(k => k.Wydawca);
            return View(ksiazka.ToList());
        }

        // GET: Ksiazki/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // GET: Ksiazki/Create
        public ActionResult Create()
        {
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie");
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo");
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa");
            return View();
        }

        // POST: Ksiazki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora,ID_Aktora,ID_Slowo_Kluczowe")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                db.Ksiazka.Add(ksiazka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // GET: Ksiazki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // POST: Ksiazki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora,ID_Aktora,ID_Slowo_Kluczowe")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ksiazka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // GET: Ksiazki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // POST: Ksiazki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            db.Ksiazka.Remove(ksiazka);
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
