using System.IO.Compression;
using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Entities.ZipObjects;
using Backups.Patterns.Visitor;

namespace Backups.Entities.Archives
{
    public class Archive : IArchive
    {
        public IStorage Collect(IReadOnlyList<IRepoObject> repoObjects, IPath path, IRepository repository, DateTime dateTime)
        {
            if (repoObjects == null)
                throw new ArgumentNullException(nameof(repoObjects));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            IPath zipFilePath = GenerateZipFilePath(path, dateTime);
            IReadOnlyList<IStorageObject> storageObjects = WriteRepoObjectsToZip(repoObjects, repository, zipFilePath);
            return CreateZipStorage(storageObjects, zipFilePath, repository);
        }

        private IPath GenerateZipFilePath(IPath path, DateTime dateTime)
        {
            string zipFileName = $"{dateTime.ToString("yyyy_MM_dd_hh_mm_ss_FFFFFF")}.zip";
            return path.Merge(zipFileName);
        }

        private IReadOnlyList<IStorageObject> WriteRepoObjectsToZip(IReadOnlyList<IRepoObject> repoObjects, IRepository repository, IPath zipFilePath)
        {
            using (Stream archiveStream = repository.Write(zipFilePath))
            {
                using var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
                var zipVisitor = new RepoObjectVisitor(zipArchive);
                foreach (IRepoObject repoObject in repoObjects)
                {
                    repoObject.Accept(zipVisitor);
                }

                return zipVisitor.GetTopStackList();
            }
        }

        private IStorage CreateZipStorage(IReadOnlyList<IStorageObject> storageObjects, IPath zipFilePath, IRepository repository)
        {
            if (storageObjects == null || storageObjects.Count == 0)
                throw new ArgumentException("No storage objects were written to the zip archive");

            return new ZipStorage(storageObjects, zipFilePath, repository);
        }
    }
}