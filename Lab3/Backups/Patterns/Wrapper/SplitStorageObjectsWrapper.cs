using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;

namespace Backups.Patterns.Wrapper;

public class SplitStorageObjectsWrapper : IStorageObjectsWrapper
{
    private IReadOnlyList<IStorageObjectsWrapper> _wrappers;

    public SplitStorageObjectsWrapper(IReadOnlyList<IStorage> storages)
    {
        _wrappers = storages.Select(x => x.GetWrapper()).ToList();
    }

    public IReadOnlyList<IRepoObject> StoragedObjects => _wrappers.SelectMany(x => x.StoragedObjects).ToList();

    public void Dispose()
    {
        foreach (IStorageObjectsWrapper wrapper in _wrappers)
        {
            wrapper.Dispose();
        }
    }
}