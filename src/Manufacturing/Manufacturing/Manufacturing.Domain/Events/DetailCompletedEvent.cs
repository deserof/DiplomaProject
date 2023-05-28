using Manufacturing.Domain.Common;
using Manufacturing.Domain.Entities;

namespace Manufacturing.Domain.Events
{
    public class DetailCompletedEvent : BaseEvent
    {
        public DetailCompletedEvent(Detail detail)
        {
            Detail = detail;
        }

        public Detail Detail { get; }
    }
}
