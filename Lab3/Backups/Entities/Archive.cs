using System.Globalization;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Archive : IArchive
{
    public IStorage Collect(IReadOnlyList<BackupObject> backupObjects, IPath path, IRepository repository, DateTime dateTime)
    {
        IPath zipFilePath = path.Merge($"{dateTime.ToString(CultureInfo.InvariantCulture)}.zip");
        RepoObjectVisitor zipVisitor;
        using (Stream archiveStream = repository.Write(zipFilePath))
        {
            var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
            zipVisitor = new RepoObjectVisitor(zipArchive);
            foreach (BackupObject backupObject in backupObjects)
            {
                backupObject.RepositoryObject.Accept(zipVisitor);
            }
        }

        IReadOnlyList<IStorageObject> storageObjects = zipVisitor.GetTopStackList();
        return new ZipStorage(storageObjects, zipFilePath, repository);
    }
}