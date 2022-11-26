namespace Backups.Interfaces;

public interface IStorageObjectsWrapper : IDisposable
{
    IReadOnlyList<IRepoObject> StoragedObjects { get; }
}