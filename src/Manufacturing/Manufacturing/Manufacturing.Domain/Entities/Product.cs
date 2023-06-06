using Manufacturing.Domain.Common;

namespace Manufacturing.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string QualityStatus { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
        public virtual ICollection<QualityControl> QualityControls { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }
}
