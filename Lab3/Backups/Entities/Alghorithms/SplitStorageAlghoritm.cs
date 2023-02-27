using Backups.Entities.Archives;
using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;

namespace Backups.Entities.Alghorithms;

public class SplitStorageAlghoritm : IStorageAlghoritm
{
    public IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime)
    {
    var archiveItems = fileSystemObjects.Select(obj => new List<IRepoObject>() { obj }).Select(objectList => archive.Collect(objectList, restorePointPath, repository, dateTime)).ToList();

    return new SplitStorage(archiveItems, restorePointPath, repository);
    }
}