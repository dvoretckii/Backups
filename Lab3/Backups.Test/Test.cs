using Backups.Entities.Alghorithms;
using Backups.Entities.Archives;
using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Models.BackupObject;
using Backups.Models.Configs;
using Backups.Services.BackupTasks;
using Xunit;
using Zio;
using Zio.FileSystems;
using Path = Backups.Entities.Paths.Path;

namespace Backups.Test
{
    public class Test
    {
        [Fact]
        public void Test1()
        {
            IFileSystem fileSystem = new MemoryFileSystem();
            IPath pathA = new Path("/Test1/a.txt");
            IPath pathB = new Path("/Test2/b.txt");
            IPath dirPathA = new Path("/Test1/");
            IPath dirPathB = new Path("/Test2/");
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

            IPath backupTaskDirName = new Path("/Test3/");
            var backupTask = new BackupTask(config, backupTaskDirName);

            backupTask.AddBackupObject(objectA);
            backupTask.AddBackupObject(objectB);

            backupTask.Backup();

            Assert.True(backupTask.BackupObjects.Count == 2);
        }
    }
}