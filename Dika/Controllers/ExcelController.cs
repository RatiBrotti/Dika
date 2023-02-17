using Dika.Models;
using Grpc.Core;
using LinqToExcel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using LinqToExcel.Extensions;
using Dika.Tools;
using Dika.Context;
using Azure.Messaging;

namespace Dika.Controllers
{
    public class ExcelController : Controller
    {
        protected DikaContext _db;

        public ExcelController(DikaContext db)
        {
            _db = db;
        }


        public async Task<IActionResult> ImportNoSKU(IFormFile exel)
        {
            ViewBag.Error = "აირჩიეთ ფაილი";
            if (exel == null) return RedirectToAction("Index", "Home", new { error = ViewBag.Error });

            var dataList = await ExcelTools.NoSKUTableConverter(exel);
            dataList = ExcelTools.AddDoubles(dataList);



            //for (int i = 0; i < dataList.Count; i++)
            //{
            //    var dbMatch=_db.Invertories.FirstOrDefault(x=>x.Barcode== dataList[i].Barcode);
            //    if (dbMatch != null)
            //    {
            //        dbMatch.QuantityOfProvider= dbMatch.QuantityOfProvider + dataList[i].QuantityOfProvider;
            //        dataList.Remove(dataList[i]);
            //    }
            //}
            //_db.Invertories.AddRange(dataList);
            //_db.SaveChanges();
            //var dbInvertories=_db.Invertories.ToList();

            return View(dataList);


        }




        public async Task<IActionResult> ImportSKU(IFormFile exel)
        {
            ViewBag.Error = "აირჩიეთ ფაილი";
            if (exel == null) return RedirectToAction("Index", "Home", new { error = ViewBag.Error });

            var dataList = await ExcelTools.SKUTableConverter(exel);
            var doubleBarcodes = ExcelTools.AddDoubles(dataList);
            if (doubleBarcodes.Count != 0)
            {
                ViewBag.Error = "ექსელის ფაილში არის დუბლირებული შტრიხკოდები: " + String.Join(",", doubleBarcodes);
                return RedirectToAction("Index", "Home", new { error = ViewBag.Error });
            }

            return View(dataList);
        }




    }
}
