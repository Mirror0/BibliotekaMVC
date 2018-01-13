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
    public class Slowa_KluczoweController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Slowa_Kluczowe
        public ActionResult Index()
        {
            return View(db.Slowo_Kluczowe.ToList());
        }

        // GET: Slowa_Kluczowe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slowo_Kluczowe slowo_Kluczowe = db.Slowo_Kluczowe.Find(id);
            if (slowo_Kluczowe == null)
            {
                return HttpNotFound();
            }
            return View(slowo_Kluczowe);
        }

        // GET: Slowa_Kluczowe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slowa_Kluczowe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Strona,Slowo")] Slowo_Kluczowe slowo_Kluczowe)
        {
            if (ModelState.IsValid)
            {
                db.Slowo_Kluczowe.Add(slowo_Kluczowe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slowo_Kluczowe);
        }

        // GET: Slowa_Kluczowe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slowo_Kluczowe slowo_Kluczowe = db.Slowo_Kluczowe.Find(id);
            if (slowo_Kluczowe == null)
            {
                return HttpNotFound();
            }
            return View(slowo_Kluczowe);
        }

        // POST: Slowa_Kluczowe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Strona,Slowo")] Slowo_Kluczowe slowo_Kluczowe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slowo_Kluczowe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slowo_Kluczowe);
        }

        // GET: Slowa_Kluczowe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slowo_Kluczowe slowo_Kluczowe = db.Slowo_Kluczowe.Find(id);
            if (slowo_Kluczowe == null)
            {
                return HttpNotFound();
            }
            return View(slowo_Kluczowe);
        }

        // POST: Slowa_Kluczowe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slowo_Kluczowe slowo_Kluczowe = db.Slowo_Kluczowe.Find(id);
            db.Slowo_Kluczowe.Remove(slowo_Kluczowe);
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
