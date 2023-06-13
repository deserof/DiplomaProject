namespace Manufacturing.Application.Reports.Commands.GenerateReport
{
    public class GenerateReportVm
    {
        public string ExcelName { get; set; }
        public MemoryStream Stream { get; set; }
    }
}
