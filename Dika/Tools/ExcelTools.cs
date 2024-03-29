﻿using Dika.Context;
using Dika.Models;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats;
using OfficeOpenXml;
using Dika.Context;

namespace Dika.Tools
{
    public class ExcelTools
    {
        protected DikaContext _db;
        public ExcelTools(DikaContext db)
        {
            _db = db;
        }
        public static List<Invertory> JoinAndSum(List<Invertory> inventories)
        {
            var groupedInventories = inventories.GroupBy(i => i.Barcode)
                                                .Select(group =>
                                                    new Invertory
                                                    {
                                                        Barcode = group.Key,
                                                        QuantityOfProvider = group.Sum(i => i.QuantityOfProvider),
                                                        Name = group.First().Name,
                                                        SKU = group.First().SKU,
                                                        Size = group.First().Size,
                                                        QuantityCounted = group.First().QuantityCounted,
                                                        Price = group.First().Price
                                                    })
                                                .ToList();
            return groupedInventories;
        }

        public static async Task<List<Invertory>> SKUTableConverter(IFormFile exel)
        {
            var inventoryList = new List<Invertory>();
            if (exel.ContentType == "application/vnd.ms-excel" || exel.FileName.Contains("xsl"))
            {

                await using var stream = new MemoryStream();
                exel.CopyTo(stream);
                stream.Position = 0;

                using var workbook = new HSSFWorkbook(stream);
                var worksheet = workbook.GetSheetAt(0);
                var rowCount = worksheet.LastRowNum;

                for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
                {
                    var row = worksheet.GetRow(rowIndex);
                    if (row.PhysicalNumberOfCells == 0 || row.Cells.All(cell => string.IsNullOrWhiteSpace(cell.ToString())))
                    {
                        continue;
                    }
                    inventoryList.Add(new Invertory
                    {
                        SKU = row.GetCell(0).ToString().Trim(),
                        Name = row.GetCell(1).ToString().Trim(),
                        Barcode = row.GetCell(2).ToString().Trim(),
                        Size = row.GetCell(3).ToString().Trim(),
                        QuantityOfProvider = (int)row.GetCell(5).NumericCellValue,
                    });
                }

            }
            return inventoryList;

        }
        public static async Task<List<Invertory>> NoSKUTableConverter(IFormFile exel)
        {
         
            var inventoryList = new List<Invertory>();
            if (exel.ContentType == "application/vnd.ms-excel" || exel.FileName.Contains("xsl"))
            {

                await using var stream = new MemoryStream();
                exel.CopyTo(stream);
                stream.Position = 0;

                using var workbook = new HSSFWorkbook(stream);
                var worksheet = workbook.GetSheetAt(0);
                var rowCount = worksheet.LastRowNum;

                for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
                {
                    var row = worksheet.GetRow(rowIndex);
                    if(row.PhysicalNumberOfCells==0 || row.Cells.All(cell=>string.IsNullOrWhiteSpace(cell.ToString())))
                    {
                        continue;
                    }
                    inventoryList.Add(new Invertory
                    {
                        Name = row.GetCell(0).ToString().Trim(),
                        Barcode = row.GetCell(3).ToString().Trim(),
                        QuantityOfProvider = Convert.ToInt32(Convert.ToDecimal((row.GetCell(4).ToString()))),
                        Price = Convert.ToDecimal(row.GetCell(5).ToString().Trim()),
                    });
                }

            }
            return inventoryList;

        }              
    }
}





// for xlsx docs
//else
//{
//    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//    using var stream = new MemoryStream();
//    await exel.CopyToAsync(stream);
//    using var reader = new ExcelPackage(stream);
//    var workSheetsCount = reader.Workbook.Worksheets.Count;
//    ExcelWorksheet worksheet = reader.Workbook.Worksheets[0];
//    var rowCount = worksheet.Dimension.Rows;
//    for (int row = 1; row < rowCount; row++)
//    {
//        inventoryList.Add(new Invertory
//        {
//            Name =worksheet.Cells[row, 1].Value.ToString().Trim(),
//            //Barcode = worksheet.Cells[row, 3].Value.ToString().Trim(),
//            //QuantityOfProvider =Convert.ToInt32(worksheet.Cells[row, 4].Value.ToString().Trim()),
//            //Price = Convert.ToInt32(worksheet.Cells[row, 4].Value),


//        });
//    }
//}
