using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

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
                else
                {
                    throw BackupsException.NullableVariable();
                }
            }, Path);
    }
}