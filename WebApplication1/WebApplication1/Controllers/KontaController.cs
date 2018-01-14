﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class KontaController : Controller
    {
        private LibDBEntities db = new LibDBEntities();
        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //Get
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Czytelnik czytelnik)
        {

            if (ModelState.IsValid)
            {
                db.Czytelnik.Add(czytelnik);
            }
            return View(czytelnik);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Czytelnik czytelnik,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var currentuser = db.Czytelnik.Where(user => user.Uzytkownik.Equals(czytelnik.Uzytkownik) && user.Haslo.Equals(czytelnik.Haslo)).FirstOrDefault();
                if (currentuser != null)
                {
                    FormsAuthentication.SetAuthCookie(currentuser.ID.ToString(), false);
                    Session["UserID"] = currentuser.ID.ToString();
                    Session["UserRole"] = currentuser.Rola.ToString();
                    return Redirect(ReturnUrl);
                }
            }

            return View(czytelnik);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}