using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Manufacturing.Application.Common.Interfaces;
using MediatR;

namespace Manufacturing.Application.Reports.Commands.GenerateReport
{
    public record GenerateReportCommand : IRequest<GenerateReportVm>
    {
        public DateTime From { get; set; }
        public DateTime End { get; set; }
    }

    public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand, GenerateReportVm>
    {
        private readonly IApplicationDbContext _context;

        public GenerateReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GenerateReportVm> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
        {
            var stream = new MemoryStream();

            using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                var sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };
                sheets.Append(sheet);
                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Fill sheet
                var row1 = new Row { RowIndex = 1 };
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Пример"),
                    DataType = CellValues.String
                });
                row1.Append(new Cell { CellValue = new CellValue("Excel"), DataType = CellValues.String });
                sheetData.Append(row1);

                var row2 = new Row { RowIndex = 2 };
                row2.Append(new Cell { CellValue = new CellValue("Файл"), DataType = CellValues.String });
                row2.Append(new Cell { CellValue = new CellValue("Скачать"), DataType = CellValues.String });
                sheetData.Append(row2);

                workbookPart.Workbook.Save();
            }

            stream.Position = 0;
            string excelName = $"Отчет-({DateTime.Now.ToString("MM-dd-yyyy")}).xlsx";

            return new GenerateReportVm
            {
                ExcelName = excelName,
                Stream = stream
            };
        }
    }
}
