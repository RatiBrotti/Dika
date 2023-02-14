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
            if (exel == null) return BadRequest("No file was Uploaded");

            var dataList = await ExcelTools.NoSKUTableConverter(exel);
            var doubleBarcodes = await ExcelTools.CheckForDoubles(dataList);
            if (doubleBarcodes.Count != 0)
            {
                ViewBag.Error = "ექსელის ფაილში არის დუბლირებული შტრიხკოდები:" + String.Join(",", doubleBarcodes);
                return View();
            }
            _db.Invertories.AddRange(dataList);
            _db.SaveChanges();

            return View(dataList);


        }




        public async Task<IActionResult> ImportSKU(IFormFile exel)
        {
            if (exel == null) return BadRequest("No file was Uploaded");


            var dataList = await ExcelTools.SKUTableConverter(exel);

            return View(dataList);
        }



    }
}
