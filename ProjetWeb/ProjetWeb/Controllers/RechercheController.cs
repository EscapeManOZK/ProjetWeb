using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
using System.Data.Entity;

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
            var musicien = bd.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays);
            return View(musicien.ToList());
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

        public ActionResult Photo(int id)
        {
            var music = bd.Musicien.Single(g => g.Code_Musicien == id);
            if (music.Photo != null)
                return File(music.Photo, "image/jpeg");
            else return null;
        }

    }
}