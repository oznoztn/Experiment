namespace Experiment.Core.Infrastructure
{
    public interface IClock
    {
        DateTime GetCurrentDateTimeUtc();
    }
}