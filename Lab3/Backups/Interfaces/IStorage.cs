namespace Backups.Interfaces;

public interface IStorage
{
    IPath Path { get; }
    IRepository Repository { get; }
    IStorageObjectsWrapper GetWrapper();
}