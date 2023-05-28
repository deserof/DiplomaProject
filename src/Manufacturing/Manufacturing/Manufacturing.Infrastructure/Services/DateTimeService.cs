using Manufacturing.Application.Common.Interfaces;

namespace Manufacturing.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
