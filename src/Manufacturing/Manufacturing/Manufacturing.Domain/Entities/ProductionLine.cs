using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Domain.Entities
{
    public class ProductionLine : BaseAuditableEntity
    {
        [Key]
        public int LineId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
    }
}
