using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Backups.Services;
using Xunit;
using Zio;
using Zio.FileSystems;
using Path = Backups.Entities.Path;

namespace Backups.Test
{
    public class Test
    {
        [Fact]
        public void AddBackupObjects_ExecuteBackupTask()
        {
            IFileSystem fileSystem = new MemoryFileSystem();
            IPath pathA = new Path("/Users/s.a.dvoretskiy/RiderProjects/dvoretckii/Lab3/Backups/a.txt");
            var pathB = new Path("/Users/s.a.dvoretskiy/RiderProjects/dvoretckii/Lab3/Backups/b.txt");
            fileSystem.CreateDirectory(pathA.GetFullPath().ToString());
            fileSystem.CreateFile(pathA.ToString()).Close();
            fileSystem.CreateDirectory(pathB.GetFullPath().ToString());
            fileSystem.CreateFile(pathB.ToString()).Close();

            IRepository repository = new RemoteRepository(fileSystem);
            IStorageAlghoritm storageAlgorithm = new SplitStorageAlghoritm();
            IArchive archive = new Archive();
            var config = new Config(storageAlgorithm, repository, archive);

            var objectA = new BackupObject(pathA, repository);
            var objectB = new BackupObject(pathB, repository);

            IPath backupTaskDirName = new Path("/Users/s.a.dvoretskiy/RiderProjects/dvoretckii/Lab3/Backups/Testing");
            var backupTask = new BackupTask(config, backupTaskDirName);

            backupTask.AddBackupObject(objectA);
            backupTask.AddBackupObject(objectB);

            backupTask.Backup();

            Assert.True(backupTask.BackupObjects.Count == 2);
        }
    }
}