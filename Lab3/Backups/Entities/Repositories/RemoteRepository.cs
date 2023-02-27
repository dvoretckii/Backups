using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Zio;
using Path = Backups.Entities.Paths.Path;

namespace Backups.Entities.Repositories;

public class RemoteRepository : IRepository
{
    private readonly IFileSystem _fileSystem;

    public RemoteRepository(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IRepoObject GetObject(IPath path)
    {
        path.ValidatePath();

        if (_fileSystem.FileExists(path.ToString()))
        {
            return CreateRepoFile(path);
        }

        if (_fileSystem.DirectoryExists(path.ToString()))
        {
            return CreateRepoFolder(path);
        }

        throw BackupsException.ElementNotFound();
    }

    public Stream Write(IPath path)
    {
        _fileSystem.CreateDirectory(path.GetFullPath().ToString());
        return _fileSystem.OpenFile(path.ToString(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
    }

    public IRepoFile CreateRepoFile(IPath path)
    {
        var openFunctor = new Func<Stream>(() => _fileSystem.OpenFile(path.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        return new RepoFile(openFunctor, path);
    }

    public IRepoFolder CreateRepoFolder(IPath path)
    {
        var getChildrenFunctor = new Func<IReadOnlyList<IRepoObject>>(() =>
            _fileSystem.EnumerateItems(path.ToString(), SearchOption.TopDirectoryOnly)
                .Select(x => GetObject(new Path(x.Path.ToString())))
                .ToList().AsReadOnly());
        return new RepoFolder(getChildrenFunctor, path);
    }
}