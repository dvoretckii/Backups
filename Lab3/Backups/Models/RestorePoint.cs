using Backups.Interfaces;

namespace Backups.Models;

public class RestorePoint
{
    public RestorePoint(DateTime dateTime, IReadOnlyList<IRepoObject> pairs)
    {
        DateTime = dateTime;
        Pairs = pairs;
    }

    public DateTime DateTime { get; }
    public IReadOnlyList<IRepoObject> Pairs { get; }
}