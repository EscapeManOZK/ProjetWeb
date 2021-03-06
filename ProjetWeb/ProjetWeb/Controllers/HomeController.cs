﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Panier()
        {
            if (!Request.IsAuthenticated) { 
                    return RedirectToAction("LogIn", "Account", new { area = "" });
                }else{
                return View();
            }
        }
    }
}