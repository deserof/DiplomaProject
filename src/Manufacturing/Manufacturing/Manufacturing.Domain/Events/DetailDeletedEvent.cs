using Manufacturing.Domain.Common;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Domain.Events
{
    public class DetailDeletedEvent : BaseEvent
    {
        public DetailDeletedEvent(Detail detail)
        {
            Detail = detail;
        }

        public Detail Detail { get; }
    }
}
