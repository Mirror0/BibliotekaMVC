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
    public class Ksiazka_EtykietaController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Ksiazka_Etykieta
        public ActionResult Index()
        {
            var ksiazka_Etykieta = db.Ksiazka_Etykieta.Include(k => k.Etykieta).Include(k => k.Ksiazka);
            return View(ksiazka_Etykieta.ToList());
        }

        // GET: Ksiazka_Etykieta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka_Etykieta ksiazka_Etykieta = db.Ksiazka_Etykieta.Find(id);
            if (ksiazka_Etykieta == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka_Etykieta);
        }

        // GET: Ksiazka_Etykieta/Create
        public ActionResult Create()
        {
            ViewBag.ID_Etykieta = new SelectList(db.Etykieta, "ID", "Etykieta1");
            ViewBag.ID_Ksiazka = new SelectList(db.Ksiazka, "ID", "Tytul");
            return View();
        }

        // POST: Ksiazka_Etykieta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Ksiazka,ID_Etykieta")] Ksiazka_Etykieta ksiazka_Etykieta)
        {
            if (ModelState.IsValid)
            {
                db.Ksiazka_Etykieta.Add(ksiazka_Etykieta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Etykieta = new SelectList(db.Etykieta, "ID", "Etykieta1", ksiazka_Etykieta.ID_Etykieta);
            ViewBag.ID_Ksiazka = new SelectList(db.Ksiazka, "ID", "Tytul", ksiazka_Etykieta.ID_Ksiazka);
            return View(ksiazka_Etykieta);
        }

        // GET: Ksiazka_Etykieta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka_Etykieta ksiazka_Etykieta = db.Ksiazka_Etykieta.Find(id);
            if (ksiazka_Etykieta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Etykieta = new SelectList(db.Etykieta, "ID", "Etykieta1", ksiazka_Etykieta.ID_Etykieta);
            ViewBag.ID_Ksiazka = new SelectList(db.Ksiazka, "ID", "Tytul", ksiazka_Etykieta.ID_Ksiazka);
            return View(ksiazka_Etykieta);
        }

        // POST: Ksiazka_Etykieta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Ksiazka,ID_Etykieta")] Ksiazka_Etykieta ksiazka_Etykieta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ksiazka_Etykieta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Etykieta = new SelectList(db.Etykieta, "ID", "Etykieta1", ksiazka_Etykieta.ID_Etykieta);
            ViewBag.ID_Ksiazka = new SelectList(db.Ksiazka, "ID", "Tytul", ksiazka_Etykieta.ID_Ksiazka);
            return View(ksiazka_Etykieta);
        }

        // GET: Ksiazka_Etykieta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka_Etykieta ksiazka_Etykieta = db.Ksiazka_Etykieta.Find(id);
            if (ksiazka_Etykieta == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka_Etykieta);
        }

        // POST: Ksiazka_Etykieta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ksiazka_Etykieta ksiazka_Etykieta = db.Ksiazka_Etykieta.Find(id);
            db.Ksiazka_Etykieta.Remove(ksiazka_Etykieta);
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
