using Backups.Entities.Archives;
using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;

namespace Backups.Entities.Alghorithms;

public class SingleStorageAlghorithm : IStorageAlghoritm
{
    public IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime)
    {
        IStorage storage = archive.Collect(fileSystemObjects, restorePointPath, repository, dateTime);
        return storage;
    }
}