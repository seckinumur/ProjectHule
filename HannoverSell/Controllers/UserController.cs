using DAL.Repo;
using DAL.VM;
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
                int id = int.Parse(Session["User"].ToString());
                var Gonder = AnalizRepo.AnalizPersonel(id);
                return View(Gonder);
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
        public ActionResult Satis()
        {
            if (Session["User"] != null)
            {
                int id = int.Parse(Session["User"].ToString());
                var Gonder = UrunRepo.StokListe();
                VMRAM.RamData = UrunRepo.SinifKoduGet();
                VMRAM.RamData2 = UrunRepo.SinifTanimiGet();
                VMRAM.SecData = UrunRepo.SectionGet();
                ViewBag.Class = "text-warning";
                ViewBag.Text = "Otomatik Stok ve Fiyat Yükleme Sistemi V.0.4 (Beta)";
                return View(Gonder);
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
        [HttpPost]
        public ActionResult UrunBul(VMUrunBulPost data)
        {
            if (data.SinifKodu == null && data.SinifTanimi == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (data.Detay != 1)
                {
                    var gonder = UrunRepo.UrunBul(data);
                    return Json(gonder, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var gonder = UrunRepo.UrunBulDetay(data);
                    return Json(gonder, JsonRequestBehavior.AllowGet);
                }
            }
        }
       
        [HttpPost]
        public ActionResult sepetekle(string Malzemekodu, double Fiyat, int Adet) //Ajax 
        {
            int Kullanici = int.Parse(Session["User"].ToString());
            var gonder = SepetRepo.SanalSepeteEkle(Kullanici, Malzemekodu, Adet, Fiyat);
            if (gonder != false)
            {
                return Json(new { success = true, responseText = "Ürün Sepete Eklendi!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Sepete Ürün Ekleme Başarısız Oldu!" }, JsonRequestBehavior.AllowGet);
            }
        }
       
        [HttpPost]
        public ActionResult UrunSepetGuncelle(string Malzemekodu, double Fiyat, int Adet) //Ajax 
        {
            int Kullanici = int.Parse(Session["User"].ToString());
            var gonder = SepetRepo.SanalSepetUrunGuncelle(Kullanici, Malzemekodu, Adet, Fiyat);
            if (gonder != false)
            {
                return Json(new { success = true, responseText = "Bu Ürün Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Ürün Güncelleme Başarısız Oldu!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult UrunSepetCikar(string Malzemekodu) //Ajax 
        {
            int Kullanici = int.Parse(Session["User"].ToString());
            var gonder = SepetRepo.SanalSepeteCikar(Kullanici, Malzemekodu);
            if (gonder != false)
            {
                return Json(new { success = true, responseText = "Bu Ürün Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Ürün Güncelleme Başarısız Oldu!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult SepetKontrol() //Ajax 
        {
            int Kullanici = int.Parse(Session["User"].ToString());
            var gonder = SepetRepo.SanalSepeteListe(Kullanici);
            return Json(gonder, JsonRequestBehavior.AllowGet);
        }
    }
}