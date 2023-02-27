using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;

namespace Backups.Models.BackupObject;

public class BackupObject : IBackupObject
{
    public BackupObject(IPath path, IRepository repository)
    {
        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }
    public IRepository Repository { get; }

    public IRepoObject RepositoryObject => Repository.GetObject(Path);
}