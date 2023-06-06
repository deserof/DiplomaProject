using Manufacturing.Domain.Common;

namespace Manufacturing.Domain.Entities
{
    public class ProductionProcess : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
    }
}
