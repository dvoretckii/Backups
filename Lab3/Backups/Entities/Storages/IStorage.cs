using Backups.Entities.Paths;
using Backups.Entities.Repositories;
using Backups.Patterns.Wrapper;

namespace Backups.Entities.Storages;

public interface IStorage
{
    IPath Path { get; }
    IRepository Repository { get; }
    IStorageObjectsWrapper GetWrapper();
}