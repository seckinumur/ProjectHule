using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HannoverSell.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var Gonder = AnalizRepo.Analiz();
                return View(Gonder);
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
    }
}