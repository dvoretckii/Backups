using Backups.Entities.Archives;
using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;

namespace Backups.Entities.Alghorithms;

public interface IStorageAlghoritm
{
    IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime);
}