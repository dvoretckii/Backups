using Backups.Exceptions;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class RemoteRepository : IRepository
{
    private readonly IFileSystem _fileSystem;

    public RemoteRepository(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IRepoObject GetObject(IPath path)
    {
        if (_fileSystem.FileExists(path.ToString()))
        {
            var openFunctor = new Func<Stream>(() => _fileSystem.OpenFile(path.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            return new RepoFile(openFunctor, path);
        }

        if (_fileSystem.DirectoryExists(path.ToString()))
        {
            var getChildrenFunctor = new Func<IReadOnlyList<IRepoObject>>(() =>
                _fileSystem.EnumerateItems(path.ToString(), SearchOption.TopDirectoryOnly)
                    .Select(x => GetObject(new Path(x.Path.ToString())))
                    .ToList().AsReadOnly());
            return new RepoFolder(getChildrenFunctor, path);
        }

        throw BackupsException.ElementNotFound();
    }

    public Stream Write(IPath path)
    {
        _fileSystem.CreateDirectory(path.GetFullPath().ToString());
        return _fileSystem.OpenFile(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
    }
}