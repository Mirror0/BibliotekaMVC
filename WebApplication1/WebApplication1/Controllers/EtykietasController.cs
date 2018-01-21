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
    public class EtykietasController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Etykietas
        public ActionResult Index()
        {
            return View(db.Etykieta.ToList());
        }

        // GET: Etykietas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etykieta etykieta = db.Etykieta.Find(id);
            if (etykieta == null)
            {
                return HttpNotFound();
            }
            return View(etykieta);
        }

        // GET: Etykietas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Etykietas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Etykieta1")] Etykieta etykieta)
        {
            if (ModelState.IsValid)
            {
                db.Etykieta.Add(etykieta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(etykieta);
        }

        // GET: Etykietas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etykieta etykieta = db.Etykieta.Find(id);
            if (etykieta == null)
            {
                return HttpNotFound();
            }
            return View(etykieta);
        }

        // POST: Etykietas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Etykieta1")] Etykieta etykieta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etykieta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(etykieta);
        }

        // GET: Etykietas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etykieta etykieta = db.Etykieta.Find(id);
            if (etykieta == null)
            {
                return HttpNotFound();
            }
            return View(etykieta);
        }

        // POST: Etykietas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Etykieta etykieta = db.Etykieta.Find(id);
            db.Etykieta.Remove(etykieta);
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
