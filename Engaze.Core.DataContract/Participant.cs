using System;


namespace Engaze.Core.DataContract
{
    public class Participant
    {
        public Participant()
        {
        }

        public Participant(Guid userId, EventAcceptanceStatus acceptanceStatus)
        {
            this.AcceptanceStatus = acceptanceStatus;
            this.UserId = userId;
        }

        public Guid UserId { get; set; }
        public EventAcceptanceStatus AcceptanceStatus { get; set; }
    }
}
