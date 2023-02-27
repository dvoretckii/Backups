namespace Backups.Entities.RepositoryObjects;

public interface IRepoFolder : IRepoObject
{
    IReadOnlyList<IRepoObject> Children { get; }
}