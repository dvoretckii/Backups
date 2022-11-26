using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SingleStorageAlghorithm : IStorageAlghoritm
{
    public IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime)
    {
        IStorage storage = archive.Collect(fileSystemObjects, restorePointPath, repository, dateTime);
        return storage;
    }
}