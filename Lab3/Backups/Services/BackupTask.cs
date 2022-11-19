using System.Globalization;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Services;

public class BackupTask
{
    private List<IRepoObject> _backupObjects = new List<IRepoObject>();
    private IPath _rootDir;

    public BackupTask(Config configuration, IPath rootDir)
    {
        Backups = new Backup();
        Configuration = configuration;
        _rootDir = rootDir;
    }

    public Backup Backups { get; }
    public Config Configuration { get; }
    public IReadOnlyList<IRepoObject> BackupObjects => _backupObjects.AsReadOnly();

    public void Backup()
    {
        DateTime backupTime = DateTime.Now;
        IPath fullRestorePointPath = _rootDir.Merge($"/{backupTime.ToString(CultureInfo.CurrentCulture).Replace(":", "_").Replace("/", "_").Replace(" ", "_")}");
        IReadOnlyList<IStorage> storages = Configuration.Alghoritm.Store(_backupObjects.AsReadOnly(), fullRestorePointPath, Configuration.Archive, Configuration.Repository, backupTime);
        var restorePoint = new RestorePoint(backupTime, BackupObjects);
        Backups.AddRestorePoint(restorePoint);
    }

    public void AddBackupObject(IRepoObject backupObject)
    {
        IRepoObject? back = _backupObjects.Find(x => x.Path.Equals(backupObject.Path));
        if (back == null)
        {
            _backupObjects.Add(backupObject);
        }
    }

    public void RemoveBackupObject(Predicate<IRepoObject> predicate)
    {
        IRepoObject? backupObject = _backupObjects.Find(predicate);
        if (backupObject == null)
        {
            throw new Exception();
        }

        _backupObjects.Remove(backupObject);
    }
}