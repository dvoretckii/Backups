using System.IO.Compression;
using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;

namespace Backups.Entities.ZipObjects;

public interface IStorageObject
{
    IPath Path { get; }
    IRepoObject ToRepoObject(ZipArchive archive);
}