using System.Globalization;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Services;

public class BackupTask : IBackupTask
{
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private IPath _rootDir;
    private Backup _backup;

    public BackupTask(Config configuration, IPath rootDir)
    {
        _backup = new Backup();
        Configuration = configuration;
        _rootDir = rootDir;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _backup.CheckSavedBackups;
    public IConfig Configuration { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public void Backup()
    {
        DateTime backupTime = DateTime.Now;
        IPath fullRestorePointPath = _rootDir.Merge($"/{backupTime.ToString("yyyy_dd_MM_hh_mm_ss")}");
        IStorage storage = Configuration.Alghoritm.Store(_backupObjects.Select(x => x.RepositoryObject).ToList(), fullRestorePointPath, Configuration.Archive, Configuration.Repository, backupTime);
        var restorePoint = new RestorePoint(backupTime, BackupObjects, storage);
        _backup.AddRestorePoint(restorePoint);
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        BackupObject? back = _backupObjects.Find(x => x.Path.Equals(backupObject.Path));
        if (back == null)
        {
            _backupObjects.Add(backupObject);
        }
    }

    public void RemoveBackupObject(Predicate<BackupObject> predicate)
    {
        BackupObject? backupObject = _backupObjects.Find(predicate);
        if (backupObject == null)
        {
            throw BackupsException.NullableVariable();
        }

        _backupObjects.Remove(backupObject);
    }
}