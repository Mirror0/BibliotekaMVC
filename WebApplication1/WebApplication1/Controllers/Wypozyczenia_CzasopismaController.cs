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
    public class Wypozyczenia_CzasopismaController : Controller
    {
        private LibDBEntities db = new LibDBEntities();
        private int stan = -1;

        // GET: Wypozyczenia_Czasopisma
        public ActionResult Index()
        {
            var wypozyczenia_Czasopisma = db.Wypozyczenia_Czasopisma.Include(w => w.Czasopismo).Include(w => w.Czytelnik);
            return View(wypozyczenia_Czasopisma.ToList());
        }

        // GET: Wypozyczenia_Czasopisma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Czasopisma wypozyczenia_Czasopisma = db.Wypozyczenia_Czasopisma.Find(id);
            if (wypozyczenia_Czasopisma == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia_Czasopisma);
        }

        // GET: Wypozyczenia_Czasopisma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Czasopisma wypozyczenia_Czasopisma = db.Wypozyczenia_Czasopisma.Find(id);
            if (wypozyczenia_Czasopisma == null)
            {
                return HttpNotFound();
            }
            stan = wypozyczenia_Czasopisma.Stan;
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Czasopisma.Stan);
            return View(wypozyczenia_Czasopisma);
        }

        // POST: Wypozyczenia_Czasopisma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Czytelnika,ID_Czasopisma,Data_Wypozyczenia,Data_Zwrotu")] Wypozyczenia_Czasopisma wypozyczenia_Czasopisma,int bstan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenia_Czasopisma).State = EntityState.Modified;
                if (bstan != wypozyczenia_Czasopisma.Stan && wypozyczenia_Czasopisma.Stan == 3)
                {
                    db.Czasopismo.Find(wypozyczenia_Czasopisma.ID_Czasopisma).Stan_Magazynowy++;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Czasopisma.Stan);
            return View(wypozyczenia_Czasopisma);
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
