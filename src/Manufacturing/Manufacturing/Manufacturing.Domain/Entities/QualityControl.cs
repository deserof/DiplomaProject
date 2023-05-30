using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class QualityControl : BaseAuditableEntity
    {
        [Key]
        public int ControlId { get; set; }
        public DateTime ControlDate { get; set; }
        public string Result { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
