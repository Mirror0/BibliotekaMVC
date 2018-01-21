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
    public class Wypozyczenia_Praca_NaukowaController : Controller
    {
        private LibDBEntities db = new LibDBEntities();
        private int stan = -1;

        // GET: Wypozyczenia_Praca_Naukowa
        public ActionResult Index()
        {
            var wypozyczenia_Praca_Naukowa = db.Wypozyczenia_Praca_Naukowa.Include(w => w.Czytelnik).Include(w => w.Praca_Naukowa);
            return View(wypozyczenia_Praca_Naukowa.ToList());
        }

        // GET: Wypozyczenia_Praca_Naukowa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Praca_Naukowa wypozyczenia_Praca_Naukowa = db.Wypozyczenia_Praca_Naukowa.Find(id);
            if (wypozyczenia_Praca_Naukowa == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia_Praca_Naukowa);
        }

        // GET: Wypozyczenia_Praca_Naukowa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Praca_Naukowa wypozyczenia_Praca_Naukowa = db.Wypozyczenia_Praca_Naukowa.Find(id);
            if (wypozyczenia_Praca_Naukowa == null)
            {
                return HttpNotFound();
            }
            stan = wypozyczenia_Praca_Naukowa.Stan;
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Praca_Naukowa.Stan);
            return View(wypozyczenia_Praca_Naukowa);
        }

        // POST: Wypozyczenia_Praca_Naukowa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Czytelnika,ID_Pracy,Data_Wypozyczenia,Data_Zwrotu,Stan")] Wypozyczenia_Praca_Naukowa wypozyczenia_Praca_Naukowa,int bstan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenia_Praca_Naukowa).State = EntityState.Modified;
                if (bstan != wypozyczenia_Praca_Naukowa.Stan && wypozyczenia_Praca_Naukowa.Stan == 3)
                {
                    db.Praca_Naukowa.Find(wypozyczenia_Praca_Naukowa.ID_Pracy).Stan_Magazynowy++;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Praca_Naukowa.Stan);
            return View(wypozyczenia_Praca_Naukowa);
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
