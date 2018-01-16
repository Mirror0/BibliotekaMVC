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
    public class KsiazkiController : Controller
    {
        private LibDBEntities db = new LibDBEntities();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var ksiazka = db.Ksiazka.Include(k => k.Autor).Include(k => k.Slowo_Kluczowe).Include(k => k.Wydawca);
            return View(ksiazka.ToList());
        }
        // GET: Ksiazki/Search
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(String SearchText)
        {
            var ksiazka = db.Ksiazka.Include(k => k.Autor).Include(k => k.Slowo_Kluczowe).Include(k => k.Wydawca);
            
            char[] values = new char[] { '&', '|', '?'};
            Boolean and = false, or = false, not = false;
            CharCheck(SearchText, ref and, ref or, ref not);

            if (!values.Any(SearchText.Contains))
            {
                {
                    ksiazka = BasicSearch(SearchText, ksiazka);
                }
            }
            else
            {
                List<string> split = SearchText.Split(values).ToList();
                int i = 0;
                var searchksiazka = db.Ksiazka.Include(k => k.Autor).Include(k => k.Slowo_Kluczowe).Include(k => k.Wydawca);
                if (and)
                {
                    ksiazka = ksiazka.Where(k => k.Tytul.ToUpper().Contains("xxx"));
                    ksiazka = ksiazka.Union(getTytulAndImie(split, searchksiazka));
                    ksiazka = ksiazka.Union(getTytulAndNazwisko(split, searchksiazka));
                    ksiazka = ksiazka.Union(getTytulAndIsbn(split, searchksiazka));
                    ksiazka = ksiazka.Union(getImieAndNazwisko(split, searchksiazka));
                    ksiazka = ksiazka.Union(getImieAndIsbn(split, searchksiazka));
                    ksiazka = ksiazka.Union(getNazwiskoAndIsbn(split, searchksiazka));
                    split.Reverse();
                    ksiazka = ksiazka.Union(getTytulAndImie(split, searchksiazka));
                    ksiazka = ksiazka.Union(getTytulAndNazwisko(split, searchksiazka));
                    ksiazka = ksiazka.Union(getTytulAndIsbn(split, searchksiazka));
                    ksiazka = ksiazka.Union(getImieAndNazwisko(split, searchksiazka));
                    ksiazka = ksiazka.Union(getImieAndIsbn(split, searchksiazka));
                    ksiazka = ksiazka.Union(getNazwiskoAndIsbn(split, searchksiazka));
                }
                else
                {
                    foreach (var part in split)
                    {

                        if (or)
                        {
                            if (i == 0)
                            {
                                ksiazka = ksiazka.Where(k => k.Tytul.ToUpper().Contains("xxx"));
                                i += 1;
                            }

                            ksiazka = ksiazka.Union(BasicSearch(part, searchksiazka));
                        }
                        else if (not)
                        {
                            searchksiazka = BasicSearch(part, searchksiazka);
                            ksiazka = ksiazka.Except(searchksiazka);

                        }
                    }
                }

            }
            
            return View(ksiazka.ToList());
        }

        private static IQueryable<Ksiazka> getTytulAndImie(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Tytul.ToUpper().Contains(one.ToUpper())
                                    && k.Autor.Imie.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> getTytulAndNazwisko(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Tytul.ToUpper().Contains(one.ToUpper())
                                                            && k.Autor.Nazwisko.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> getTytulAndIsbn(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Tytul.ToUpper().Contains(one.ToUpper())
                                                            && k.ISBN.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> getImieAndNazwisko(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Autor.Imie.ToUpper().Contains(one.ToUpper())
                                                            && k.Autor.Nazwisko.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> getImieAndIsbn(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Autor.Imie.ToUpper().Contains(one.ToUpper())
                                                            && k.ISBN.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> getNazwiskoAndIsbn(List<string> part, IQueryable<Ksiazka> searchksiazka)
        {
            string one = part.ElementAt(0);
            string two = part.ElementAt(1);
            return searchksiazka.Where(k => k.Autor.Nazwisko.ToUpper().Contains(one.ToUpper())
                                                            && k.ISBN.ToUpper().Contains(two.ToUpper()));
        }

        private static IQueryable<Ksiazka> BasicSearch(string SearchText, IQueryable<Ksiazka> searchksiazka)
        {
            return searchksiazka.Where(k => k.Tytul.ToUpper().Contains(SearchText.ToUpper())
                                                        || k.Autor.Imie.ToUpper().Contains(SearchText.ToUpper())
                                                        || k.Autor.Nazwisko.ToUpper().Contains(SearchText.ToUpper())
                                                        || k.ISBN.ToUpper().Contains(SearchText.ToUpper()));
        }

        private static void CharCheck(string SearchText, ref bool and, ref bool or, ref bool not)
        {
            if (SearchText.Contains("&")) { and = true; }
            else if (SearchText.Contains("|")) { or = true; }
            else if (SearchText.Contains("?")) { not = true; }
        }

        // GET: Ksiazki/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // GET: Ksiazki/Create
        public ActionResult Create()
        {
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie");
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo");
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa");
            return View();
        }

        // POST: Ksiazki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora,ID_Aktora,ID_Slowo_Kluczowe")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                db.Ksiazka.Add(ksiazka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // GET: Ksiazki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // POST: Ksiazki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tytul,ISBN,Strony,ID_Wydawcy,ID_Autora,ID_Aktora,ID_Slowo_Kluczowe")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ksiazka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Autora = new SelectList(db.Autor, "ID", "Imie", ksiazka.ID_Autora);
            ViewBag.ID_Slowo_Kluczowe = new SelectList(db.Slowo_Kluczowe, "ID", "Slowo", ksiazka.ID_Slowo_Kluczowe);
            ViewBag.ID_Wydawcy = new SelectList(db.Wydawca, "ID", "Nazwa", ksiazka.ID_Wydawcy);
            return View(ksiazka);
        }

        // GET: Ksiazki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // POST: Ksiazki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ksiazka ksiazka = db.Ksiazka.Find(id);
            db.Ksiazka.Remove(ksiazka);
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
