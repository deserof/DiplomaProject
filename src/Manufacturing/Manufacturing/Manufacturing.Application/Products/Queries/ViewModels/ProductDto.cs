using Manufacturing.Application.Common.Mappings;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Application.Products.Queries.ViewModels
{
    public class ProductDto : IMapFrom<Product>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string QualityStatus { get; set; }
    }
}
