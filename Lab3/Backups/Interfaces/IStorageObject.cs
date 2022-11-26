using System.IO.Compression;

namespace Backups.Interfaces;

public interface IStorageObject
{
    IPath Path { get; }
    IRepoObject ToRepoObject(ZipArchive archive);
}