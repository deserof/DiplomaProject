using Manufacturing.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain.Entities
{
    public class ProductFile : BaseAuditableEntity
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public byte[] FileContent { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public enum FileType
    {
        Drawing,
        Model3D
    }
}
