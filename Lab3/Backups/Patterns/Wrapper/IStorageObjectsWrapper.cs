using Backups.Entities.RepositoryObjects;

namespace Backups.Patterns.Wrapper;

public interface IStorageObjectsWrapper : IDisposable
{
    IReadOnlyList<IRepoObject> StoragedObjects { get; }
}