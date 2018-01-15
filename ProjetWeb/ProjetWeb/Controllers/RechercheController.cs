using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;

namespace ProjetWeb.Controllers
{
    public class RechercheController : Controller
    {
        private Classique_Web_2017Entities bd = new Classique_Web_2017Entities();
        // GET: Recherche
        public ActionResult Epoque()
        {
            return View();
        }
        public ActionResult Artiste()
        {
            return View();
        }
        public ActionResult Genre()
        {
            return View();
        }
        public ActionResult Oeuvre()
        {
            return View();
        }
        public ActionResult Instrument()
        {
            return View();
        }

    }
}