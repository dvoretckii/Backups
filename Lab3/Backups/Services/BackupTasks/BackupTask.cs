using System.Globalization;
using Backups.Entities.Paths;
using Backups.Entities.Storages;
using Backups.Exceptions;
using Backups.Models.BackupObject;
using Backups.Models.Backups;
using Backups.Models.Configs;
using Backups.Models.RestorePoints;

namespace Backups.Services.BackupTasks
{
    public class BackupTask : IBackupTask
    {
        private List<BackupObject> _backupObjects = new List<BackupObject>();
        private IPath _rootDir;
        private Backup _backup;

        public BackupTask(IConfig configuration, IPath rootDir)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _rootDir = rootDir ?? throw new ArgumentNullException(nameof(rootDir));
            _backup = new Backup();
        }

        public IReadOnlyList<IRestorePoint> RestorePoints => _backup.CheckSavedBackups;
        public IConfig Configuration { get; }
        public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

        public void Backup()
        {
            DateTime backupTime = DateTime.Now;
            IPath fullRestorePointPath = GetFullRestorePointPath(backupTime);
            IStorage storage = GetStorage(fullRestorePointPath, backupTime);
            RestorePoint restorePoint = new RestorePoint.Builder()
                .SetDateTime(backupTime)
                .AddPairs(BackupObjects)
                .SetStorage(storage)
                .Build();
            _backup.AddRestorePoint(restorePoint);
        }

        public void AddBackupObject(BackupObject backupObject)
        {
            if (_backupObjects.Any(x => x.Path.Equals(backupObject.Path)))
            {
                return;
            }

            _backupObjects.Add(backupObject);
        }

        public void RemoveBackupObject(Predicate<BackupObject> predicate)
        {
            BackupObject backupObject = _backupObjects.Find(predicate) ?? throw BackupsException.NullableVariable();
            _backupObjects.Remove(backupObject);
        }

        public IPath GetFullRestorePointPath(DateTime backupTime)
        {
            string dateTimeString = backupTime.ToString("yyyy_dd_MM_hh_mm_ss", CultureInfo.InvariantCulture);
            return _rootDir.Merge(dateTimeString);
        }

        public IStorage GetStorage(IPath fullRestorePointPath, DateTime backupTime)
        {
            var repoObjects = BackupObjects.Select(backupObject => backupObject.RepositoryObject).ToList();

            IStorage storage = Configuration.Alghoritm.Store(repoObjects, fullRestorePointPath, Configuration.Archive, Configuration.Repository, backupTime);
            return storage;
        }
    }
}