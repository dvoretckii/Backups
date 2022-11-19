using System.Globalization;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Services;

public class BackupTask
{
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private IPath _rootDir;

    public BackupTask(Config configuration, IPath rootDir)
    {
        Backups = new Backup();
        Configuration = configuration;
        _rootDir = rootDir;
    }

    public Backup Backups { get; }
    public Config Configuration { get; }
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public void Backup()
    {
        DateTime backupTime = DateTime.Now;
        IPath fullRestorePointPath = _rootDir.Merge($"/{backupTime.ToString(CultureInfo.InvariantCulture)}");
        IReadOnlyList<IStorage> storages = Configuration.Alghoritm.Store(_backupObjects.AsReadOnly(), fullRestorePointPath, Configuration.Archive, Configuration.Repository, backupTime);
        var list = new List<RestorePointPair>();
        foreach ((BackupObject, IStorage) pair in _backupObjects.Zip(storages))
        {
            var restorePointPair = new RestorePointPair(pair.Item1, pair.Item2);
        }

        list.AsReadOnly();
        var restorePoint = new RestorePoint(backupTime, list);
        Backups.AddRestorePoint(restorePoint);
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        // смотрим есть ли такой путь дефолтное добавление
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
            throw new Exception();
        }

        _backupObjects.Remove(backupObject);
    }
}