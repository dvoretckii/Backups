using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlghoritm
{
    IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime);
}