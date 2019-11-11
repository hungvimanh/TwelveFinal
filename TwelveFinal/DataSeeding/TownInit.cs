﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class TownInit : CommonInit
    {
        public TownInit(TFContext _context) : base(_context)
        {

        }

        public void Init()
        {
            List<TownDAO> ethnics = LoadFromExcel("../../../DataSeeding.xlsx");
            DbContext.AddRange(ethnics);
        }
        private List<TownDAO> LoadFromExcel(string path)
        {
            List<TownDAO> excelTemplates = new List<TownDAO>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[7];
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    string provinceCode = worksheet.Cells[i, 1].Value?.ToString();
                    string districtCode = worksheet.Cells[i, 2].Value?.ToString();
                    string townCode = worksheet.Cells[i, 3].Value?.ToString();
                    string townName = worksheet.Cells[i, 4].Value?.ToString();

                    if (provinceCode.Length < 2)
                    {
                        provinceCode = "0" + provinceCode;
                    }
                    if (districtCode.Length < 2)
                    {
                        districtCode = "0" + districtCode;
                    }
                    if (townCode.Length < 2)
                    {
                        townCode = "0" + townCode;
                    }
                    if (townName.Contains("("))
                    {
                        townName = townName.Split(" (")[0];
                    }

                    TownDAO excelTemplate = new TownDAO()
                    {
                        Id = CreateGuid("Town" + provinceCode + districtCode + townCode),
                        DistrictId = CreateGuid("District" + provinceCode + districtCode),
                        Code = townCode,
                        Name = townName
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
    }
}

