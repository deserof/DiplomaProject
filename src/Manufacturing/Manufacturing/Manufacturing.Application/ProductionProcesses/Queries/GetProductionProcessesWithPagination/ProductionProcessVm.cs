using Manufacturing.Application.Common.Mappings;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Application.ProductionProcesses.Queries.GetProductionProcessesWithPagination
{
    public class ProductionProcessVm : IMapFrom<ProductionProcess>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
