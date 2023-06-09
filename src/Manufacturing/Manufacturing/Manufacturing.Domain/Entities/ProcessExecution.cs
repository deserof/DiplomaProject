using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class ProcessExecution : BaseAuditableEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("ProductionProcess")]
        public int ProcessId { get; set; }
        public virtual ProductionProcess ProductionProcess { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("ProcessFile")]
        public int? ProcessFileId { get; set; }
        public virtual ProductFile ProcessFile { get; set; }

        [ForeignKey("ProcessPhoto")]
        public int? ProcessPhotoId { get; set; }
        public virtual ProductFile ProcessPhoto { get; set; }
    }
}
