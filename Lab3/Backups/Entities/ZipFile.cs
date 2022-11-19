using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IStorageObject
{
    public ZipFile(IPath zipStoragePath)
    {
        Path = zipStoragePath;
    }

    public IPath Path { get; }
}