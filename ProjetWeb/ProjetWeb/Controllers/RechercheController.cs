﻿using System;
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
        [Route("RechercheArtiste/{lettre}")]
        public ActionResult RechercheArtiste(string lettre)
        {
            var musicien = bd.Musicien.Where(m => m.Nom_Musicien.StartsWith(lettre));

            if (musicien == null)
            {
                return HttpNotFound();
            }
            return View(musicien.ToList());

        }
        public ActionResult Genre()
        {
            var musicien = bd.Genre.OrderBy(m => m.Libelle_Abrege);
            return View(musicien.ToList());
        }
        public ActionResult DetailGenre(int id)
        {
            var musicien = bd.Album.OrderBy(m => m.Titre_Album).Where(m => m.Code_Album == id);
            return View(musicien.ToList());
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
        public ActionResult DetailMusicien(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musicien musicien = bd.Musicien.Find(id);
            if (musicien == null)
            {
                return HttpNotFound();
            }
            return View(musicien);
        }

        public ActionResult DetailMusicienO(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var musicien = (from data in bd.Musicien
                                 join composer in bd.Composer on data.Code_Musicien equals composer.Code_Musicien
                                join oeuv in bd.Oeuvre on composer.Code_Oeuvre equals oeuv.Code_Oeuvre
                                 where(oeuv.Code_Oeuvre == id)
                                 select data);
            if (musicien == null)
            {
                return HttpNotFound();
            }

            return View(musicien.ToList());
        }

        public ActionResult Album(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = (from d in bd.Album
                         join disque in bd.Disque on d.Code_Album equals disque.Code_Album
                         join CompositionDi in bd.Composition_Disque on disque.Code_Disque equals CompositionDi.Code_Disque
                         join enreg in bd.Enregistrement on CompositionDi.Code_Morceau equals enreg.Code_Morceau
                         join compo in bd.Composition on enreg.Code_Composition equals compo.Code_Composition
                         join compoOeu in bd.Composition_Oeuvre on compo.Code_Composition equals compoOeu.Code_Composition
                         join oeuvre in bd.Oeuvre on compoOeu.Code_Oeuvre equals oeuvre.Code_Oeuvre
                         join composer in bd.Composer on oeuvre.Code_Oeuvre equals composer.Code_Oeuvre
                         where (composer.Code_Musicien == id)
                         select d).Distinct();
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album.ToList());
        }
        public ActionResult Photo(int id)
        {
            var music = bd.Musicien.Single(g => g.Code_Musicien == id);
            if (music.Photo != null)
                return File(music.Photo, "image/jpeg");
            else return null;
        }
        public ActionResult Pochette(int id)
        {
            var album = bd.Album.Single(g => g.Code_Album == id);
            if (album.Pochette != null)
                return File(album.Pochette, "image/jpeg");
            else return null;
        }

        public ActionResult Instru(int id)
        {
            var instru = bd.Instrument.Single(g => g.Code_Instrument == id);
            if (instru.Image != null)
                return File(instru.Image, "image/jpeg");
            else return null;
        }
        public ActionResult Audio(int? id)
        {
            var sons = bd.Enregistrement.Single(e => e.Code_Morceau == id);
            return File(sons.Extrait, "mp3");
        }

        public ActionResult NomInstrument(int id) {

            var instrument = bd.Instrument.Where(m => m.Code_Instrument == id);
            return View(instrument);
        }

        public ActionResult DetailInstru(int? id)
        {
            var oeuvre = bd.Instrumentation.Include(m=> m.Oeuvre).Include(m=>m.Instrument).Where(m=>m.Code_Instrument==id).OrderBy(m=>m.Oeuvre.Titre_Oeuvre); 
            return View(oeuvre);
        }

        public ActionResult OeuvreMusicien(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var oeuvres = (from data in bd.Oeuvre
                            join composer in bd.Composer on data.Code_Oeuvre equals composer.Code_Oeuvre
                           join oeuv in bd.Musicien on composer.Code_Musicien equals oeuv.Code_Musicien
                           where (oeuv.Code_Musicien == id)
                            select data);
            if (oeuvres == null)
            {
                return HttpNotFound();
            }

            return View(oeuvres.ToList());
        }
    }
}