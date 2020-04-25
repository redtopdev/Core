
namespace Engaze.Core.DataContract
{
    public enum EventType
    {
        SHAREMYLOACTION = 100,
        TRACKBUDDY = 200,
        QUIK = 300,
        GENERAL = 400
    }

    public enum EventState
    {
        NOT_STARTED = 1,
        IN_PROGRESS,
        COMPLETED
    }

    public enum EventAcceptanceState
    {
        Pending = -1,
        Rejected = 0,
        Accepted
    }

    public enum TrackingStatus
    {
        NOT_STARTED = 1,
        IN_PROGRESS,
        COMPLETED
    }

    public enum RecurrenceFrequency
    {
        Daily = 1,  //Meeting which recurs daily.
        Weekly,     //Meeting which recurs weekly.
        Monthly,    //Meeting which recurs monthly
        Yearly,     //Meeting which recurs Yearly.
    }
}
