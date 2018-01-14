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
    public class AktorzyController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Aktorzy
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Aktor.ToList());
        }

        // GET: Aktorzy/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktor aktor = db.Aktor.Find(id);
            if (aktor == null)
            {
                return HttpNotFound();
            }
            return View(aktor);
        }

        // GET: Aktorzy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aktorzy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko")] Aktor aktor)
        {
            if (ModelState.IsValid)
            {
                db.Aktor.Add(aktor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aktor);
        }

        // GET: Aktorzy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktor aktor = db.Aktor.Find(id);
            if (aktor == null)
            {
                return HttpNotFound();
            }
            return View(aktor);
        }

        // POST: Aktorzy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko")] Aktor aktor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aktor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aktor);
        }

        // GET: Aktorzy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktor aktor = db.Aktor.Find(id);
            if (aktor == null)
            {
                return HttpNotFound();
            }
            return View(aktor);
        }

        // POST: Aktorzy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aktor aktor = db.Aktor.Find(id);
            db.Aktor.Remove(aktor);
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
