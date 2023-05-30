using Manufacturing.Application.Common.Mappings;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Application.Details.Queries.ViewModels
{
    public class DetailDto : IMapFrom<Detail>
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }
}
