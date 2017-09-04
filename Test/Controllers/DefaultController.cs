using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using DAL.Repo;
using Entity;

namespace Test.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExcellTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcellTest(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            string[] Liste = new string[20];
                            for (int i = 2; i < 22; i++)
                            {
                                try
                                {
                                    Liste[i - 2] = workSheet.Cells[rowIterator, i].Value.ToString();
                                }
                                catch
                                {
                                    Liste[i - 2] = null;
                                }
                            }
                            UrunRepo.ExcelKaydet(Liste);
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult Liste() //Ajax
        {
            var gonder = UrunRepo.Liste();
            var jsonResult = Json(gonder, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult Anasayfa()
        {
            return View();
        }
    }
}