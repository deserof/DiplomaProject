using Manufacturing.Domain.Common;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Domain.Events
{
    public class DetailCreatedEvent : BaseEvent
    {
        public DetailCreatedEvent(Detail detail)
        {
            Detail = detail;
        }

        public Detail Detail { get; }
    }
}
