using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    IReadOnlyList<RestorePoint> RestorePoints { get; }
    IConfig Configuration { get; }
    void Backup();
    void AddBackupObject(BackupObject backupObject);
    void RemoveBackupObject(Predicate<BackupObject> predicate);
}