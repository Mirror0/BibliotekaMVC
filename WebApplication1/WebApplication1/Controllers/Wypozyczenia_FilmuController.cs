using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using FluentValidation.Results;

namespace WebApplication1.Controllers
{
    public class Wypozyczenia_FilmuController : Controller
    {
        private LibDBEntities db = new LibDBEntities();
        private int stan = -1;

        // GET: Wypozyczenia_Filmu
        public ActionResult Index()
        {
            var wypozyczenia_Filmu = db.Wypozyczenia_Filmu.Include(w => w.Czytelnik).Include(w => w.Film);
            return View(wypozyczenia_Filmu.ToList());
        }

        // GET: Wypozyczenia_Filmu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Filmu wypozyczenia_Filmu = db.Wypozyczenia_Filmu.Find(id);
            if (wypozyczenia_Filmu == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia_Filmu);
        }

        // GET: Wypozyczenia_Filmu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenia_Filmu wypozyczenia_Filmu = db.Wypozyczenia_Filmu.Find(id);
            if (wypozyczenia_Filmu == null)
            {
                return HttpNotFound();
            }
            ViewBag.BStan = wypozyczenia_Filmu.Stan;
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Filmu.Stan);
            return View(wypozyczenia_Filmu);
        }

        // POST: Wypozyczenia_Filmu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Czytelnika,ID_Filmu,Data_Wypozyczenia,Data_Zwrotu,Stan")] Wypozyczenia_Filmu wypozyczenia_Filmu,int bstan)
        {
            FilmValidator validator = new FilmValidator();
            ValidationResult result = validator.Validate(wypozyczenia_Filmu);

            if (!result.IsValid)
            {
                ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Filmu.Stan);
                ViewBag.Error = result.Errors[0].ErrorMessage;
                return View(wypozyczenia_Filmu);
            }


            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenia_Filmu).State = EntityState.Modified;
                if (bstan != wypozyczenia_Filmu.Stan && wypozyczenia_Filmu.Stan == 3)
                {
                    db.Film.Find(wypozyczenia_Filmu.ID_Filmu).Stan_Magazynowy++;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stan = new SelectList(db.Stan, "ID", "Opis", wypozyczenia_Filmu.Stan);
            return View(wypozyczenia_Filmu);
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
