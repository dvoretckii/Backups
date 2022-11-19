namespace Backups.Interfaces;

public interface IRepository
{
    IRepoObject GetObject(IPath path);
    Stream Write(IPath path);
}