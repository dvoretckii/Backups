using Backups.Models.RestorePoints;

namespace Backups.Models.Backups;

public interface IBackup
{
    RestorePoint AddRestorePoint(RestorePoint restorePoint);
}