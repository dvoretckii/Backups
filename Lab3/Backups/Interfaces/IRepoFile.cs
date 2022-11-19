namespace Backups.Interfaces;

public interface IRepoFile : IRepoObject
{
    Stream Stream { get; }
}