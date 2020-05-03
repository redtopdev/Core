using System;

namespace Engaze.Core.DataContract
{
    public class EventStoreEvent
    {
        public Guid EventId { get; set; }
        public OccuredEventType EventType { get; set; }
        public string Data { get; set; }
    }
}
