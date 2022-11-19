using Backups.Models;

namespace Backups.Interfaces;

public interface IArchive
{
    IStorage Collect(IReadOnlyList<IRepoObject> repoObjects, IPath path, IRepository repository, DateTime dateTime);
}