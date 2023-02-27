using System.IO.Compression;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.ZipObjects;

namespace Backups.Patterns.Wrapper;

public class ZipStorageObjectsWrapper : IStorageObjectsWrapper
{
    private IReadOnlyList<IStorageObject> _storageObjects;
    private Stream _archiveStream;
    private ZipArchive _archive;

    public ZipStorageObjectsWrapper(IReadOnlyList<IStorageObject> storageObjects, Stream archiveStream)
    {
        _archiveStream = archiveStream;
        _archive = new ZipArchive(_archiveStream);
        _storageObjects = storageObjects;
    }

    public IReadOnlyList<IRepoObject> StoragedObjects => _storageObjects.Select(x => x.ToRepoObject(_archive)).ToList();

    public void Dispose()
    {
        _archiveStream.Dispose();
        _archive.Dispose();
    }
}