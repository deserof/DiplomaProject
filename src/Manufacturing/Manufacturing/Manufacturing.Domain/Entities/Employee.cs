using Manufacturing.Domain.Common;

namespace Manufacturing.Domain.Entities
{
    public class Employee : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }

        public virtual ICollection<ProductionOrder> ProductionOrders { get; set; }
        public virtual ICollection<QualityControl> QualityControls { get; set; }
    }
}
