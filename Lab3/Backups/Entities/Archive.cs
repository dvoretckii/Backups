using System.Globalization;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Archive : IArchive
{
    public IStorage Collect(IReadOnlyList<IRepoObject> repoObjects, IPath path, IRepository repository, DateTime dateTime)
    {
        IPath zipFilePath = path.Merge($"{dateTime.ToString("yyyy_MM_dd_hh_mm_ss_FFFFFF")}.zip");
        RepoObjectVisitor zipVisitor;
        using (Stream archiveStream = repository.Write(zipFilePath))
        {
            using var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
            zipVisitor = new RepoObjectVisitor(zipArchive);
            foreach (IRepoObject backupObject in repoObjects)
            {
                backupObject.Accept(zipVisitor);
            }
        }

        IReadOnlyList<IStorageObject> storageObjects = zipVisitor.GetTopStackList();
        return new ZipStorage(storageObjects, zipFilePath, repository);
    }
}