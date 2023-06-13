namespace Manufacturing.Application.Processes.Queries.GetProcessPhoto
{
    public class ProcessPhotoVm
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public byte[] ImageData { get; set; }
    }
}
