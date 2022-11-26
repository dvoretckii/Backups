using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class LocalRepository : IRepository
{
    public IRepoObject GetObject(IPath path)
    {
        if (File.Exists(path.ToString()))
        {
            var functor = new Func<Stream>(() => File.OpenRead(path.ToString()));
            return new RepoFile(functor, path);
        }

        if (Directory.Exists(path.ToString()))
        {
            var childrenFunctor = new Func<IReadOnlyList<IRepoObject>>(
                () => Directory.EnumerateFileSystemEntries(path.ToString(), "*", SearchOption.TopDirectoryOnly)
                    .Select(x => GetObject(new Path(x)))
                    .ToList().AsReadOnly());
            return new RepoFolder(childrenFunctor, path);
        }

        throw BackupsException.ElementNotFound();
    }

    public Stream Write(IPath path)
    {
        Directory.CreateDirectory(path.GetFullPath().ToString());
        return File.OpenWrite(path.ToString());
    }
}