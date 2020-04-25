using System;
using System.Collections.Generic;

namespace Engaze.Core.DataContract
{
    public class Event
    {
        public string Name { get; set; }

        public EventType EventType { get; set; }

        public string Description { get; set; }

        public Guid InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Duration Duration { get; set; }

        public Duration Tracking { get; set; }

        public EventState EventState { get; set; }

        public IEnumerable<Guid> Participants { get; set; }

        public Location Destination { get; set; }

        public Reminder Reminder { get; set; }

        public Recurrence Recurrence { get; set; }


        /*
          EventoContract ev = new EventoContract()
          {
              Description = "testevent",
              Destination = new Location()
              {
                  Address = "BTM, Bangalore 560029",
                  Latitude = 76.76732M,
                  Longitude = 112.4537M,
                  Name = "Atul Address"
              },
              Duration = new Duration()
              {
                  Enabled = true,
                  Period = "minutes",
                  TimeInterval = 120
              },
              EndTime = DateTime.UtcNow.AddMinutes(120),
              EventState = EventState.NOT_STARTED,
              EventType = EventType.GENERAL,
              InitiatorId = Guid.Parse("af4f7839-bb7a-400a-9d0b-24ef49aba05f"),
              InitiatorName = "atul",
              Name = "TestEvent",
              Participnats = new List<Guid>() { Guid.Parse("47cdee07-3e19-4c3f-b250-e9751c7c092c"), Guid.Parse("c70fcabb-de74-4faa-96b8-34899b97ec88") },
              Recurrence = new Recurrence()
              {
                  ActualStartTime = DateTime.UtcNow,
                  Count = 100,
                  DaysOfWeek = "1,3,5",
                  Frequency = 2,
                  FrequencyType = RecurrenceFrequency.Weekly,
                  Remaining = 99
              },
              Reminder = new Reminder()
              {
                  NotificationType = "alarm",
                  Period = "minutes",
                  ReminderOffsetInMinute = 10L,
                  TimeInterval = 100
              },
              StartTime = DateTime.UtcNow,
              Tracking = new Duration()
              {
                  Enabled = true,
                  Period = "minutes",
                  TimeInterval = 120
              }
          };
          string evestr = JsonConvert.SerializeObject(ev); */


    }
}
