using DAL.Repo;
using DAL.VM;
using Entity.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HannoverSell.Controllers
{
    public class AdminController : Controller
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
        public ActionResult DatabaseSystem()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
        [HttpPost]
        public ActionResult DatabaseSystem(FormCollection formCollection)
        {
            if (Session["User"] != null)
            {
                if (Request != null)
                {
                    List<Urun> urunler = new List<Urun>();
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        try
                        {
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                int noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    urunler.Add(new Urun
                                    {
                                        Marka = (workSheet.Cells[rowIterator, 2].Value ?? String.Empty).ToString(),
                                        Model = (workSheet.Cells[rowIterator, 3].Value ?? String.Empty).ToString(),
                                        SinifKodu = (workSheet.Cells[rowIterator, 4].Value ?? String.Empty).ToString(),
                                        SinifTanimi = (workSheet.Cells[rowIterator, 5].Value ?? String.Empty).ToString(),
                                        MalzemeKodu = (workSheet.Cells[rowIterator, 6].Value ?? String.Empty).ToString(),
                                        Section1 = (workSheet.Cells[rowIterator, 7].Value ?? String.Empty).ToString(),
                                        Section2 = (workSheet.Cells[rowIterator, 8].Value ?? String.Empty).ToString(),
                                        Section3 = (workSheet.Cells[rowIterator, 9].Value ?? String.Empty).ToString(),
                                        Section4 = (workSheet.Cells[rowIterator, 10].Value ?? String.Empty).ToString(),
                                        Section5 = (workSheet.Cells[rowIterator, 11].Value ?? String.Empty).ToString(),
                                        Section6 = (workSheet.Cells[rowIterator, 12].Value ?? String.Empty).ToString(),
                                        Section7 = (workSheet.Cells[rowIterator, 13].Value ?? String.Empty).ToString(),
                                        Section8 = (workSheet.Cells[rowIterator, 14].Value ?? String.Empty).ToString(),
                                        Section9 = (workSheet.Cells[rowIterator, 15].Value ?? String.Empty).ToString(),
                                        Section10 = (workSheet.Cells[rowIterator, 16].Value ?? String.Empty).ToString(),
                                        Section11 = (workSheet.Cells[rowIterator, 17].Value ?? String.Empty).ToString(),
                                        Section12 = (workSheet.Cells[rowIterator, 18].Value ?? String.Empty).ToString(),
                                        Section13 = (workSheet.Cells[rowIterator, 19].Value ?? String.Empty).ToString(),
                                        Section14 = (workSheet.Cells[rowIterator, 20].Value ?? String.Empty).ToString(),
                                        Section15 = (workSheet.Cells[rowIterator, 21].Value ?? String.Empty).ToString()
                                    });
                                }
                            }
                            bool sonuc = UrunRepo.ExcelKaydet(urunler);
                            if (sonuc == true)
                            {
                                TempData["UyariTipi"] = "panel panel-success";
                                TempData["Sonuc"] = "Database Başarıyla Güncelleştirildi!";
                                return View();
                            }
                            else
                            {
                                TempData["UyariTipi"] = "panel panel-danger";
                                TempData["Sonuc"] = "Geçerli Bir Excel Dosyası Seçilmedi! Database Güncelleme Başarılı Olmadı!";
                                return View();
                            }
                        }
                        catch
                        {
                            TempData["UyariTipi"] = "panel panel-danger";
                            TempData["Sonuc"] = "Geçerli Bir Excel Dosyası Seçilmedi! Database Güncelleme Başarılı Olmadı!";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["UyariTipi"] = "panel panel-warning";
                        TempData["Sonuc"] = "Geçerli Dosyada Veri Yok yada Veri İçeren Bir Dosya Tipi Değil!";
                        return View();
                    }
                }
                else
                {
                    TempData["UyariTipi"] = "panel panel-warning";
                    TempData["Sonuc"] = "Dosya Seçilmedi!";
                    return View();
                }
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
        public ActionResult StokIslemleri()
        {
            if (Session["User"] != null)
            {
                var Gonder = UrunRepo.StokListe();
                RamData = UrunRepo.SinifKoduGet();
                RamData2 = UrunRepo.SinifTanimiGet();
                SecData = UrunRepo.SectionGet();
                return View(Gonder);
            }
            else
            {
                TempData["UyariTipi"] = "text-danger";
                TempData["Sonuc"] = "Tarayıcıda Oturum Süreniz Dolmuş! Lütfen Tekrar Oturum Açın!";
                return RedirectToAction("Logon", "Login");
            }
        }
        [HttpGet]
        public ActionResult SinifKoduAjax(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(RamData.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult SinifTanimiAjax(string q) //Ajax
        {
            if (q.Length >= 5)
            {
                return Json(RamData2.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
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
                var gonder = UrunRepo.UrunBul(data);
                return Json(gonder, JsonRequestBehavior.AllowGet);
            }
        }
        private static List<string> RamData { get; set; }
        private static List<string> RamData2 { get; set; }
        private static VMSection SecData { get; set; }
        [HttpGet]
        public ActionResult Section1(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section1.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section2(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section2.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section3(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section3.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section4(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section4.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section5(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section5.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section6(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section6.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section7(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section7.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section8(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section8.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section9(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section9.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section10(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section10.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section11(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section11.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section12(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section12.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section13(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section13.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section14(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section14.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Section15(string q) //Ajax
        {
            if (q.Length >= 4)
            {
                return Json(SecData.Section15.Where(p => p.Contains(q.ToUpper().Trim())).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult StokEkle(VMStokEkle data) //Ajax
        {
            bool sonuc = UrunRepo.StokEkleAjax(data);
            if (sonuc !=false)
            {
                return Json(new { success = true, responseText = "Stok Başarıyla Güncellendi!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Stok Güncelleme Başarılı olmadı!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}