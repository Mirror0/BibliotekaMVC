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
    public class Prace_NaukoweController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Prace_Naukowe
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var praca_Naukowa = db.Praca_Naukowa.Include(p => p.Autor);
            return View(praca_Naukowa.ToList());
        }

        // GET: Prace_Naukowe/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praca_Naukowa praca_Naukowa = db.Praca_Naukowa.Find(id);
            if (praca_Naukowa == null)
            {
                return HttpNotFound();
            }
            return View(praca_Naukowa);
        }

        // GET: Prace_Naukowe/Create
        public ActionResult Create()
        {
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie");
            return View();
        }

        // POST: Prace_Naukowe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tytul,ID_Autora")] Praca_Naukowa praca_Naukowa)
        {
            if (ModelState.IsValid)
            {
                db.Praca_Naukowa.Add(praca_Naukowa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", praca_Naukowa.ID_Autora);
            return View(praca_Naukowa);
        }

        // GET: Prace_Naukowe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praca_Naukowa praca_Naukowa = db.Praca_Naukowa.Find(id);
            if (praca_Naukowa == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", praca_Naukowa.ID_Autora);
            return View(praca_Naukowa);
        }

        // POST: Prace_Naukowe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tytul,ID_Autora")] Praca_Naukowa praca_Naukowa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(praca_Naukowa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", praca_Naukowa.ID_Autora);
            return View(praca_Naukowa);
        }

        // GET: Prace_Naukowe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praca_Naukowa praca_Naukowa = db.Praca_Naukowa.Find(id);
            if (praca_Naukowa == null)
            {
                return HttpNotFound();
            }
            return View(praca_Naukowa);
        }

        // POST: Prace_Naukowe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Praca_Naukowa praca_Naukowa = db.Praca_Naukowa.Find(id);
            db.Praca_Naukowa.Remove(praca_Naukowa);
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
