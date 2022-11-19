using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFolder : IStorageObject
{
    private IReadOnlyList<IStorageObject> _children;

    public ZipFolder(IReadOnlyList<IStorageObject> children, IPath path)
    {
        _children = children;
        Path = path;
    }

    public IPath Path { get; }
}