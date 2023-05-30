using Manufacturing.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Manufacturing.Infrastructure.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
