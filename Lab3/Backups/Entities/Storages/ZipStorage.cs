using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.ZipObjects;
using Backups.Patterns.Wrapper;

namespace Backups.Entities.Storages;

public class ZipStorage : IStorage
{
    private IReadOnlyList<IStorageObject> _children;

    public ZipStorage(IReadOnlyList<IStorageObject> children, IPath path, IRepository repository)
    {
        _children = children;
        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }
    public IRepository Repository { get; }
    public IStorageObjectsWrapper GetWrapper()
    {
        Stream stream = ((IRepoFile)Repository.GetObject(Path)).Stream;
        return new ZipStorageObjectsWrapper(_children, stream);
    }
}