using Manufacturing.Domain.Common;

namespace Manufacturing.Domain.Entities
{
    public class ProductionLine : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
    }
}
