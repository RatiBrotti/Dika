﻿using Dika.Models;
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
            var joinedBarcodes = ExcelTools.JoinAndSum(dataList);

            foreach (var item in joinedBarcodes)
            {
                var dbmatch = _db.Invertories.FirstOrDefault(x => x.Barcode == item.Barcode);
                if (dbmatch != null)
                {
                    dbmatch.QuantityOfProvider += item.QuantityOfProvider;
                }
                else
                {
                    var newInventory = new Invertory
                    {
                        Barcode = item.Barcode,
                        QuantityOfProvider = item.QuantityOfProvider,
                        Name = item.Name,
                        SKU = item.SKU,
                        Size = item.Size,
                        Price = item.Price,
                        QuantityCounted = item.QuantityCounted,

                    };
                    _db.Invertories.Add(newInventory);
                }
            }
            _db.SaveChanges();


            return RedirectToAction("Index","Home");

        }




        public async Task<IActionResult> ImportSKU(IFormFile exel)
        {
            ViewBag.Error = "აირჩიეთ ფაილი";
            if (exel == null) return RedirectToAction("Index", "Home", new { error = ViewBag.Error });

            var dataList = await ExcelTools.SKUTableConverter(exel);
            var joinedBarcodes = ExcelTools.JoinAndSum(dataList);

            foreach (var item in joinedBarcodes)
            {
                var dbmatch = _db.Invertories.FirstOrDefault(x => x.Barcode == item.Barcode);
                if (dbmatch != null)
                {
                    dbmatch.QuantityOfProvider += item.QuantityOfProvider;
                }
                else
                {
                    var newInventory = new Invertory
                    {
                        Barcode = item.Barcode,
                        QuantityOfProvider = item.QuantityOfProvider,
                        Name = item.Name,
                        SKU = item.SKU,
                        Size = item.Size,
                        Price = item.Price,
                        QuantityCounted = item.QuantityCounted,

                    };
                    _db.Invertories.Add(newInventory);
                }
            }
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }




    }
}
