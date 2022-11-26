using Backups.Models;

namespace Backups.Interfaces;

public interface IBackup
{
    RestorePoint AddRestorePoint(RestorePoint restorePoint);
}