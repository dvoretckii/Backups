using Backups.Interfaces;

namespace Backups.Models;

public class BackupObject
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