using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class ProcessExecution : BaseAuditableEntity
    {
        [Key]
        public int ExecutionId { get; set; }
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

        [ForeignKey("ProductionLine")]
        public int LineId { get; set; }
        public virtual ProductionLine ProductionLine { get; set; }
    }
}
