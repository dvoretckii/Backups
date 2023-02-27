using Backups.Entities.Paths;
using Backups.Entities.Repositories;

namespace Backups.Models.BackupObject;

public interface IBackupObject
{
    IPath Path { get; }
    IRepository Repository { get; }
}