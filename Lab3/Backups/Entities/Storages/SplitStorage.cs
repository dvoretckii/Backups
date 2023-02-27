using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Patterns.Wrapper;

namespace Backups.Entities.Storages;

public class SplitStorage : IStorage
{
    private IReadOnlyList<IStorage> _storages;

    public SplitStorage(IReadOnlyList<IStorage> storages, IPath path, IRepository repository)
    {
        _storages = storages;
        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }
    public IRepository Repository { get; }
    public IStorageObjectsWrapper GetWrapper()
    {
        return new SplitStorageObjectsWrapper(_storages);
    }
}