using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Domain.Entities
{
    public class ProductionProcess : BaseAuditableEntity
    {
        [Key]
        public int ProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
    }
}
