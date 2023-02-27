using Backups.Entities.Storages;
using Backups.Models.BackupObject;

namespace Backups.Models.RestorePoints
{
    public class RestorePoint : IRestorePoint
    {
        private RestorePoint() { }

        public DateTime DateTime { get; private set; }
        public IReadOnlyList<IBackupObject>? Pairs { get; private set; }
        public IStorage? Storage { get; private set; }

        public class Builder
        {
            private readonly List<BackupObject.BackupObject> pairs = new List<BackupObject.BackupObject>();
            private DateTime dateTime;
            private IStorage? storage;

            public Builder SetDateTime(DateTime dateTime)
            {
                this.dateTime = dateTime;
                return this;
            }

            public Builder AddPair(BackupObject.BackupObject backupObject)
            {
                pairs.Add(backupObject);
                return this;
            }

            public Builder AddPairs(IEnumerable<BackupObject.BackupObject> backupObjects)
            {
                pairs.AddRange(backupObjects);
                return this;
            }

            public Builder SetStorage(IStorage storage)
            {
                this.storage = storage;
                return this;
            }

            public RestorePoint Build()
            {
                if (dateTime == default(DateTime))
                    throw new InvalidOperationException("DateTime must be set");

                if (storage == null)
                    throw new InvalidOperationException("Storage must be set");

                var restorePoint = new RestorePoint();
                restorePoint.DateTime = dateTime;
                restorePoint.Pairs = pairs.AsReadOnly();
                restorePoint.Storage = storage;
                return restorePoint;
            }
        }
    }
}