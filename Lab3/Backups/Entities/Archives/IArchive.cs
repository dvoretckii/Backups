using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;

namespace Backups.Entities.Archives;

public interface IArchive
{
    IStorage Collect(IReadOnlyList<IRepoObject> repoObjects, IPath path, IRepository repository, DateTime dateTime);
}