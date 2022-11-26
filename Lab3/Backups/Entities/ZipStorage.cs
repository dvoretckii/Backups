using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

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