using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SplitStorageAlghoritm : IStorageAlghoritm
{
    public IStorage Store(IReadOnlyList<IRepoObject> fileSystemObjects, IPath restorePointPath, IArchive archive, IRepository repository, DateTime dateTime)
    {
        return new SplitStorage(fileSystemObjects.Select(x => archive.Collect(new List<IRepoObject> { x }, restorePointPath, repository, dateTime)).ToList(), restorePointPath, repository);
    }
}