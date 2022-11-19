using Backups.Interfaces;

namespace Backups.Models;

public class RestorePointPair
{
    public RestorePointPair(BackupObject backupObject, IStorage storage)
    {
        BackupObject = backupObject;
        Storage = storage;
    }

    public BackupObject BackupObject { get; }
    public IStorage Storage { get; }
}