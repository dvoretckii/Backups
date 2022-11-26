using Backups.Interfaces;

namespace Backups.Models;

public class RestorePoint
{
    public RestorePoint(DateTime dateTime, IReadOnlyList<BackupObject> pairs, IStorage storage)
    {
        DateTime = dateTime;
        Pairs = pairs;
        Storage = storage;
    }

    public DateTime DateTime { get; }
    public IReadOnlyList<BackupObject> Pairs { get; }
    public IStorage Storage { get; }
}