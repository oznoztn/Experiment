namespace Experiment.Core.Infrastructure
{
    public class SystemClock : IClock
    {
        public DateTime GetCurrentDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
