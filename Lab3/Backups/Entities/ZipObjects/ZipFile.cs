using System.IO.Compression;
using Backups.Entities.Paths;
using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;

namespace Backups.Entities.ZipObjects;

public class ZipFile : IStorageObject
{
    public ZipFile(IPath zipStoragePath)
    {
        Path = zipStoragePath;
    }

    public IPath Path { get; }

    public IRepoObject ToRepoObject(ZipArchive archive)
    {
        return new RepoFile(
            () =>
            {
                ZipArchiveEntry? entry = archive.GetEntry(Path.GetName().ToString());
                if (entry != null)
                {
                    return entry.Open();
                }

                throw BackupsException.NullableVariable();
            }, Path);
    }
}