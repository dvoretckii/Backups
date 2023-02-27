using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;
using Path = Backups.Entities.Paths.Path;

namespace Backups.Entities.Repositories;

public class LocalRepository : IRepository
{
    public IRepoObject GetObject(IPath path)
    {
        path.ValidatePath();

        if (File.Exists(path.ToString()))
        {
            return CreateRepoFile(path);
        }

        if (Directory.Exists(path.ToString()))
        {
            return CreateRepoFolder(path);
        }

        throw new InvalidOperationException("Object not found.");
    }

    public Stream Write(IPath path)
    {
        Directory.CreateDirectory(path.GetFullPath().ToString());
        return File.OpenWrite(path.ToString());
    }

    public IRepoFile CreateRepoFile(IPath path)
    {
        var functor = new Func<Stream>(() => File.OpenRead(path.ToString()));
        return new RepoFile(functor, path);
    }

    public IRepoFolder CreateRepoFolder(IPath path)
    {
        var childrenFunctor = new Func<IReadOnlyList<IRepoObject>>(
            () => Directory.EnumerateFileSystemEntries(path.ToString(), "*", SearchOption.TopDirectoryOnly)
                .Select(GetObjectFromEntryPath)
                .ToList().AsReadOnly());
        return new RepoFolder(childrenFunctor, path);
    }

    public IRepoObject GetObjectFromEntryPath(string entryPath)
    {
        return GetObject(new Path(entryPath));
    }
}