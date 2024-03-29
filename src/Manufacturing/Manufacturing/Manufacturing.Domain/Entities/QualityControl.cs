﻿using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class QualityControl : BaseAuditableEntity
    {
        public DateTime ControlDate { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
