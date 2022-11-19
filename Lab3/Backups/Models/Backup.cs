namespace Backups.Models
{
    public class Backup
    {
        private readonly List<RestorePoint> _savedBackup = new List<RestorePoint>();

        public IReadOnlyList<RestorePoint> CheckSavedBackups => _savedBackup.AsReadOnly();

        public RestorePoint AddRestorePoint(RestorePoint restorePoint)
        {
            _savedBackup.Add(restorePoint);
            return restorePoint;
        }
    }
}