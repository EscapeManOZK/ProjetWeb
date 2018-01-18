using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetWeb.Models;
using System.Net;
using System.Data.Entity;

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
            var oeuvre = bd.Oeuvre.OrderBy(m => m.Titre_Oeuvre);
            return View(oeuvre.ToList());
        }
        public ActionResult Instrument()
        {
            var Instrument = bd.Instrument.OrderBy(m => m.Nom_Instrument);
            return View(Instrument.ToList());
        }
        public ActionResult DetailOeuvre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oeuvre oeuvre = bd.Oeuvre.Find(id);
            if (oeuvre == null)
            {
                return HttpNotFound();
            }
            return View(oeuvre);
        }
        public ActionResult Photo(int id)
        {
            var music = bd.Musicien.Single(g => g.Code_Musicien == id);
            if (music.Photo != null)
                return File(music.Photo, "image/jpeg");
            else return null;
        }
        public ActionResult Audio(int? id)
        {
            var sons = bd.Enregistrement.Single(e => e.Code_Morceau == id);
            return File(sons.Extrait, "mp3");
        }
        public ActionResult DetailInstru(int? id)
        {
            var oeuvre = bd.Instrumentation.Include(m=> m.Oeuvre).Include(m=>m.Instrument).Where(m=>m.Code_Instrument==id).OrderBy(m=>m.Oeuvre.Titre_Oeuvre);
            return View(oeuvre);
        }
    }
}