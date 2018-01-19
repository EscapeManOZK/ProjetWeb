using System;
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
                    return RedirectToAction("LogIn", "Account", new { returnUrl = "Panier" });
                }else{
                return View();
            }
        }
        public ActionResult AjoutPanier(int Code)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account", new { returnUrl = "" });
            }
            else
            {
                if (Session["Panier"] == null)
                {
                    Session["Panier"] = new int[] { Code };
                }
                else {
                    int[] a = (int[])Session["Panier"];
                    a.ToList().Add(Code);
                    Session["Panier"] = a;
                }
                return RedirectToAction("", "Home", new { });
            }
            
        }
    }
}