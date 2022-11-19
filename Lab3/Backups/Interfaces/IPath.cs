namespace Backups.Interfaces;

public interface IPath
{
    string ToString();
    IPath Merge(IPath path);
    IPath Merge(string path);
    IPath GetFullPath();
    IPath GetName();
}