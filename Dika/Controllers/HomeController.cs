using Dika.Context;
using Dika.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using System.Diagnostics;

namespace Dika.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DikaContext _db;
        public HomeController(ILogger<HomeController> logger, DikaContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index(string error)
        {
            ViewBag.error = error;
            return View(_db.Invertories.ToList());
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entity = _db.Invertories.Find(id);

            return View(entity);
        }
        [HttpPost]
        public IActionResult Delete(Invertory entity)
        {
            _db.Invertories.Remove(entity);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _db.Invertories.FirstOrDefault(x => x.Id == id);
            return View(entity);
        }
        [HttpGet]
        public IActionResult AddProduct(string error)
        {
            ViewBag.error = error;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Invertory entity)
        {
            var checkForExistence = _db.Invertories.FirstOrDefault(x => x.Barcode == entity.Barcode);
            if (checkForExistence != null)
            {
                ViewBag.error = checkForExistence.Barcode + ": შტრიხკოდით ჩანაწერი უკვე არსებობს!";
                return RedirectToAction("AddProduct", new { ViewBag.error });
            }
            else if (entity.Barcode == null || entity.Barcode.Length < 13)
            {
                ViewBag.error = "შტრიხკოდის ფორმატი არასწორია";
                return RedirectToAction("AddProduct", new { ViewBag.error });
            }
            _db.Invertories.Add(entity);
            _db.SaveChanges();
            return RedirectToAction("AddProduct");
        }
        [HttpGet]
        public IActionResult Search(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                ViewBag.error = "საძიებო ტექსტი ცარიელია";
                return RedirectToAction("Index", new { ViewBag.error });
            }
            var entities = _db.Invertories.Where(x => x.Barcode.Contains(search.Trim()) || x.SKU.Contains(search.Trim()) || x.Name.Contains(search.ToUpper().Trim())).ToList();

            return View(entities);
        }
        [HttpGet]
        public IActionResult Inventorying()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddByBarcode(string barcode)
        {
            var entity = _db.Invertories.FirstOrDefault(x => x.Barcode == barcode);
            entity.QuantityCounted++;
            _db.SaveChanges();

            return RedirectToAction("Inventorying", entity);
        }

        [HttpPost]
        public IActionResult Edit(Invertory entity)
        {
            _db.Invertories.Update(entity);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}