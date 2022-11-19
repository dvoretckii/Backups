using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SingleStorageAlghorithm : IStorageAlghoritm
{
    public IReadOnlyList<IStorage> Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime)
    {
        IStorage storage = archive.Collect(fileSystemObjects, restorePointPath, repository, dateTime);
        return Enumerable.Repeat(storage, fileSystemObjects.Count).ToList().AsReadOnly();
    }
}