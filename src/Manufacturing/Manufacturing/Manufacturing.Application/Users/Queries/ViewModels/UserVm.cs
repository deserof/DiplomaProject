using Manufacturing.Application.Common.Mappings;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Application.Users.Queries.ViewModels
{
    public class UserVm : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
    }
}
