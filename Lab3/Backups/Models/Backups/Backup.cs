using Backups.Models.RestorePoints;

namespace Backups.Models.Backups
{
    public class Backup : IBackup
    {
        private readonly List<RestorePoint> _savedBackup = new List<RestorePoint>();

        public IReadOnlyList<RestorePoint> CheckSavedBackups => _savedBackup.AsReadOnly();

        public RestorePoint AddRestorePoint(RestorePoint restorePoint)
        {
            _savedBackup.Add(restorePoint);
            return restorePoint;
        }

        public void AddRestorePoints(IEnumerable<RestorePoint> restorePoints)
        {
            _savedBackup.AddRange(restorePoints);
        }
    }
}