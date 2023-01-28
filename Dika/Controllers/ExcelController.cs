using Dika.Models;
using Grpc.Core;
using LinqToExcel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Dika.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExcelController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Import()
        {
            var file = Request.Form.Files[0];
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "App_Data", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var excel = new ExcelQueryFactory(filePath);
            var rows = (from row in excel.WorksheetRangeNoHeader("A1", "Z100") select row).ToList();

            // do the rest of your import logic here, such as saving to the database
            return View(rows);
        }
              

    }
}
