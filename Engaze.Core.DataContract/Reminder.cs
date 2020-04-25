

namespace Engaze.Core.DataContract
{
    public class Reminder
    {
        public int TimeInterval { get; set; }

        public string Period { get; set; }

        public string NotificationType { get; set; }

        public long ReminderOffsetInMinute { get; set; }
    }
}
