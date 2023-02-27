using System.IO.Compression;
using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;

namespace Backups.Entities.ZipObjects;

public class ZipFolder : IStorageObject
{
    private IReadOnlyList<IStorageObject> _children;

    public ZipFolder(IReadOnlyList<IStorageObject> children, IPath path)
    {
        _children = children;
        Path = path;
    }

    public IPath Path { get; }
    public IRepoObject ToRepoObject(ZipArchive archive)
    {
        return new RepoFolder(() => _children.Select(x => x.ToRepoObject(archive)).ToList(), Path);
    }
}