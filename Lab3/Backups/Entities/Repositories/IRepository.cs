using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;

namespace Backups.Entities.Repositories;

public interface IRepository
{
    IRepoObject GetObject(IPath path);
    Stream Write(IPath path);
    IRepoFolder CreateRepoFolder(IPath path);
    IRepoFile CreateRepoFile(IPath path);
}