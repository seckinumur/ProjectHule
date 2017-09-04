using DAL.Repo;
using DAL.VM;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HannoverSell.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(VMKullanici data)
        {
            bool kontrol = LoginRepo.Kontrol(data);
            if (kontrol = !false)
            {
                var Kullanici = LoginRepo.Login(data);
                if (Kullanici.Admin != true)
                {
                    Session["User"] = Kullanici.KullanicilarID;
                    Session["Name"] = Kullanici.KullaniciAdi;
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    Session["User"] = Kullanici.KullanicilarID;
                    Session["Name"] = Kullanici.KullaniciAdi;
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Kullanıcı Adı Yada Parolası Hatalı!";
                return View();
            }
        }
        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Logon");
        }
    }
}