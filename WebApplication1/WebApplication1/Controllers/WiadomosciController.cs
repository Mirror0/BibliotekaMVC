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
    public class WiadomosciController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        // GET: Wiadomosci
        public ActionResult Index()
        {
            var wiadomosci = db.Wiadomosci.Include(w => w.Czytelnik);
            return View(wiadomosci.ToList());
        }

        // GET: Wiadomosci/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiadomosci wiadomosci = db.Wiadomosci.Find(id);
            if (wiadomosci == null)
            {
                return HttpNotFound();
            }
            return View(wiadomosci);
        }

        // GET: Wiadomosci/Create
        public ActionResult Create()
        {
            ViewBag.ID_Czytelnika = new SelectList(db.Czytelnik, "ID", "Imie");
            return View();
        }

        // POST: Wiadomosci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tresc,ID_Czytelnika")] Wiadomosci wiadomosci)
        {
            if (ModelState.IsValid)
            {
                db.Wiadomosci.Add(wiadomosci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Czytelnika = new SelectList(db.Czytelnik, "ID", "Imie", wiadomosci.ID_Czytelnika);
            return View(wiadomosci);
        }

        // GET: Wiadomosci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiadomosci wiadomosci = db.Wiadomosci.Find(id);
            if (wiadomosci == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Czytelnika = new SelectList(db.Czytelnik, "ID", "Imie", wiadomosci.ID_Czytelnika);
            return View(wiadomosci);
        }

        // POST: Wiadomosci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tresc,ID_Czytelnika")] Wiadomosci wiadomosci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wiadomosci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Czytelnika = new SelectList(db.Czytelnik, "ID", "Imie", wiadomosci.ID_Czytelnika);
            return View(wiadomosci);
        }

        // GET: Wiadomosci/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiadomosci wiadomosci = db.Wiadomosci.Find(id);
            if (wiadomosci == null)
            {
                return HttpNotFound();
            }
            return View(wiadomosci);
        }

        // POST: Wiadomosci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wiadomosci wiadomosci = db.Wiadomosci.Find(id);
            db.Wiadomosci.Remove(wiadomosci);
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
