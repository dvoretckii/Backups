namespace Backups.Interfaces;

public interface IRepoFolder : IRepoObject
{
    IReadOnlyList<IRepoObject> Children { get; }
}