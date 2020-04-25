using System;


namespace Engaze.Core.DataContract
{
    public class Participant
    {       
        public Participant(Guid userID, EventAcceptanceState acceptanceState)
        {
            this.AcceptanceState = acceptanceState;
            this.UserId = userID;
        }

        public Guid UserId { get; private set; }
        public EventAcceptanceState AcceptanceState { get; private set; }
    }
}
