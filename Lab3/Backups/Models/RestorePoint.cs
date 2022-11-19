namespace Backups.Models;

public class RestorePoint
{
    public RestorePoint(DateTime dateTime, IReadOnlyList<RestorePointPair> pairs)
    {
        DateTime = dateTime;
        Pairs = pairs;
    }

    public DateTime DateTime { get; }
    public IReadOnlyList<RestorePointPair> Pairs { get; }
}