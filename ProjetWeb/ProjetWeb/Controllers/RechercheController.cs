using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
<<<<<<< HEAD
=======
using System.Data.Entity;
>>>>>>> cf95d7f291b1b143324236f3a1b4650880f0b3ac

namespace ProjetWeb.Controllers
{
    public class RechercheController : Controller
    {
        private Classique_Web_2017Entities bd = new Classique_Web_2017Entities();
        // GET: Recherche
        public ActionResult Epoque()
        {
            var oeuvre = bd.Oeuvre.OrderBy(m => m.Annee).Where(m => m.Annee.Value>0);
            return View(oeuvre.ToList());
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
        public ActionResult DetailOeuvre()
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