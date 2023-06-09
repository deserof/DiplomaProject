namespace Manufacturing.Application.Processes.Queries.GetProcessesByProductId
{
    public class ProcessVm
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ProductionProcessId { get; set; }
        public string Name { get; set; }
        public string ProductionProcessDescription { get; set; }
        public string Description { get; set; }
    }
}
