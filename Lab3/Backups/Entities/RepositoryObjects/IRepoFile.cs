namespace Backups.Entities.RepositoryObjects;

public interface IRepoFile : IRepoObject
{
    Stream Stream { get; }
}