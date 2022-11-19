using Backups.Models;

namespace Backups.Interfaces;

public interface IArchive
{
    IStorage Collect(IReadOnlyList<BackupObject> backupObjects, IPath path, IRepository repository, DateTime dateTime);
}