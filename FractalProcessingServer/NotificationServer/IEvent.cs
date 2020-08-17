using System.Collections.Generic;

namespace NotificationServer
{
    public interface IEvent
    {
    }

    public class EventGroup : List<IEvent>, IEvent
    {
    }
}
