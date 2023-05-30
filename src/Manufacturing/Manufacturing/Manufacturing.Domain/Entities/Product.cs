using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Manufacturing.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string QualityStatus { get; set; }

        public virtual ICollection<ProcessExecution> ProcessExecutions { get; set; }
        public virtual ICollection<QualityControl> QualityControls { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }
}
