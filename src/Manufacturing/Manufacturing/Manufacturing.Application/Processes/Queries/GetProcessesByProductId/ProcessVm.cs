namespace Manufacturing.Application.Processes.Queries.GetProcessesByProductId
{
    public class ProcessVm
    {
        public int ProcessExecutionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ProductionProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // public TimeSpan Duration { get; set; }

        //public virtual ProductionLine ProductionLine { get; set; }
    }
}
