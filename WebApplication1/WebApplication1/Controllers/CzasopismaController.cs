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
    public class CzasopismaController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Czasopisma
        public ActionResult Index()
        {
            var czasopismo = db.Czasopismo.Include(c => c.Autor).Include(c => c.Wydawca);
            return View(czasopismo.ToList());
        }

        // GET: Czasopisma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czasopismo czasopismo = db.Czasopismo.Find(id);
            if (czasopismo == null)
            {
                return HttpNotFound();
            }
            return View(czasopismo);
        }

        // GET: Czasopisma/Create
        public ActionResult Create()
        {
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie");
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa");
            return View();
        }

        // POST: Czasopisma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora")] Czasopismo czasopismo)
        {
            if (ModelState.IsValid)
            {
                db.Czasopismo.Add(czasopismo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", czasopismo.ID_Autora);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", czasopismo.ID_Wydawcy);
            return View(czasopismo);
        }

        // GET: Czasopisma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czasopismo czasopismo = db.Czasopismo.Find(id);
            if (czasopismo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", czasopismo.ID_Autora);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", czasopismo.ID_Wydawcy);
            return View(czasopismo);
        }

        // POST: Czasopisma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora")] Czasopismo czasopismo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(czasopismo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", czasopismo.ID_Autora);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", czasopismo.ID_Wydawcy);
            return View(czasopismo);
        }

        // GET: Czasopisma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czasopismo czasopismo = db.Czasopismo.Find(id);
            if (czasopismo == null)
            {
                return HttpNotFound();
            }
            return View(czasopismo);
        }

        // POST: Czasopisma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Czasopismo czasopismo = db.Czasopismo.Find(id);
            db.Czasopismo.Remove(czasopismo);
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
