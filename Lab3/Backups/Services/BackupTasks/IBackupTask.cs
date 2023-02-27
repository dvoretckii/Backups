using Backups.Entities.Paths;
using Backups.Entities.Storages;
using Backups.Models.Configs;
using Backups.Models.RestorePoints;

namespace Backups.Services.BackupTasks;

public interface IBackupTask
{
    IReadOnlyList<IRestorePoint> RestorePoints { get; }
    IConfig Configuration { get; }
    void Backup();
    IPath GetFullRestorePointPath(DateTime backupTime);
    IStorage GetStorage(IPath fullRestorePointPath, DateTime backupTime);
}