using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class ProductionOrder : BaseAuditableEntity
    {
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
