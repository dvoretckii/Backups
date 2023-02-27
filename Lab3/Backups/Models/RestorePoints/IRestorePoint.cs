using Backups.Entities.Storages;
using Backups.Models.BackupObject;

namespace Backups.Models.RestorePoints;

public interface IRestorePoint
{
    DateTime DateTime { get; }
    public IReadOnlyList<IBackupObject>? Pairs { get; }
    public IStorage? Storage { get; }
}