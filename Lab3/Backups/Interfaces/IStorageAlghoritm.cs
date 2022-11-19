using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlghoritm
{
    IReadOnlyList<IStorage> Store(IReadOnlyList<BackupObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime);
}