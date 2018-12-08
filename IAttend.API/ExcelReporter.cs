using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API
{
    public class ExcelReporter : IDisposable
    {

        private readonly string _sWebRootFolder ;
        private readonly string _sFileName ;
        private readonly string _filePath;

        private readonly IWorkbook _workbook;

        public string ExcelFilePath => _filePath;

        public ExcelReporter(string sWebRootFolder,string sFileName)
        {
            _sWebRootFolder = sWebRootFolder;
            _sFileName = sFileName;

            _filePath = Path.Combine(_sWebRootFolder, _sFileName);

            _workbook = new XSSFWorkbook();
        }

        public ISheet CreateSheet(string sheetName)
        {
            return _workbook.CreateSheet(sheetName);
        }

        public IRow AddRow(ref ISheet sheet)
        {
            return sheet.CreateRow(0);
        }


        public void AddValue(ref IRow row,int index,string value)
        {
            row.CreateCell(index).SetCellValue(value);
        }

        public void WriteFileStream()
        {
            using (var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            {
                _workbook.Write(fs);
            }
        }

        //string sWebRootFolder = _hostingEnvironment.WebRootPath;
        //string sFileName = @"demo.xlsx";
        //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
        //FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

        //using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
        //{

        //    IWorkbook workbook;
        //    workbook = new XSSFWorkbook();

        //    ISheet excelSheet = workbook.CreateSheet("Demo");
        //    IRow row = excelSheet.CreateRow(0);

        //    row.CreateCell(0).SetCellValue("ID");
        //    row.CreateCell(1).SetCellValue("Name");
        //    row.CreateCell(2).SetCellValue("Age");

        //    row = excelSheet.CreateRow(1);
        //    row.CreateCell(0).SetCellValue(1);
        //    row.CreateCell(1).SetCellValue("Kane Williamson");
        //    row.CreateCell(2).SetCellValue(29);

        //    row = excelSheet.CreateRow(2);
        //    row.CreateCell(0).SetCellValue(2);
        //    row.CreateCell(1).SetCellValue("Martin Guptil");
        //    row.CreateCell(2).SetCellValue(33);

        //    row = excelSheet.CreateRow(3);
        //    row.CreateCell(0).SetCellValue(3);
        //    row.CreateCell(1).SetCellValue("Colin Munro");
        //    row.CreateCell(2).SetCellValue(23);

        //    workbook.Write(fs);
        //}

        //var memory = new MemoryStream();
        //using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
        //{
        //    await stream.CopyToAsync(memory);
        //}
        //memory.Position = 0;
        public void Dispose()
        {

        }
    }
}
