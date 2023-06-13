using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Manufacturing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                Columns columns = new Columns();
                uint columnCount = 6;
                for (uint i = 1; i <= columnCount; i++)
                {
                    Column column = new Column
                    {
                        Min = i,
                        Max = i,
                        BestFit = true,
                        CustomWidth = true,
                        Width = 30
                    };
                    columns.Append(column);
                }
                worksheetPart.Worksheet.InsertBefore(columns, sheetData);

                // Fill sheet

                var products = _context.Products
                    .Include(x => x.ProcessExecutions)
                    .ThenInclude(x => x.ProductionProcess)
                    .Where(x => x.CreationDate > request.From && x.CreationDate < request.End);
                uint index = 1;

                var row1 = new Row { RowIndex = index++ };
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Название"),
                    DataType = CellValues.String,
                    StyleIndex = 0,
                });
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Описание"),
                    DataType = CellValues.String,
                });
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Качество"),
                    DataType = CellValues.String,
                });
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Дата создания"),
                    DataType = CellValues.String,
                });
                row1.Append(new Cell
                {
                    CellValue = new CellValue("Процессы"),
                    DataType = CellValues.String,
                });
                sheetData.Append(row1);

                foreach (var product in products)
                {
                    var row = new Row {RowIndex = index++};
                    row.Append(new Cell
                    {
                        CellValue = new CellValue(product.Name),
                        DataType = CellValues.String,
                    });
                    row.Append(new Cell
                    {
                        CellValue = new CellValue(product.Description),
                        DataType = CellValues.String,
                    });
                    row.Append(new Cell
                    {
                        CellValue = new CellValue(product.QualityStatus),
                        DataType = CellValues.String,
                    });
                    row.Append(new Cell
                    {
                        CellValue = new CellValue(product.CreationDate.ToString("MM-dd-yyyy")),
                        DataType = CellValues.String,
                    });

                    var processes = product.ProcessExecutions;
                    sheetData.Append(row);
                    foreach (var process in processes)
                    {
                        var nestedRow = new Row { RowIndex = index++ };
                        nestedRow.Append(new Cell());
                        nestedRow.Append(new Cell());
                        nestedRow.Append(new Cell());
                        nestedRow.Append(new Cell());
                        nestedRow.Append(new Cell
                        {
                            CellValue = new CellValue(process.ProductionProcess.Name),
                            DataType = CellValues.String,
                        });
                        sheetData.Append(nestedRow);
                    }
                }

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
